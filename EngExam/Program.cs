using Application;
using Application.Caching;
using Application.Handler;
using Application.Handler.InterfaceHandler;
using Application.Interface;
using Application.Repositories;
using Application.UnitOfWork;
using AutoMapper;
using Domain.Entity;
using Domain.Enums;
using EngExam.Extensions;
using EngExam.Middlewares;
using EngExam.OptionsModels;
using Infrastructure.AI;
using Infrastructure.Authentication.Services;
using Infrastructure.Caching;
using Infrastructure.Repositories.SQLServer;
using Infrastructure.Repositories.SQLServer.DataContext;
using Infrastructure.Repositories.SQLServer.Mappers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7077");
                          //policy.AllowAnyOrigin();
                          policy.AllowAnyHeader();
                          policy.AllowCredentials();
                          policy.AllowAnyMethod();
                      });
});
RegisterServicesForSecurity(builder.Configuration, builder.Services);
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.Cookie.Name = "EngExam_Token";
//    options.Cookie.HttpOnly = true;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
//    options.Events.OnRedirectToAccessDenied = context =>
//    {
//        context.Response.StatusCode = StatusCodes.Status403Forbidden;
//        return Task.CompletedTask;
//    };
//});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
RegisterServicesForApp(builder.Configuration, builder.Services);


builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(MyAllowSpecificOrigins);
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "api");
    });
    app.UseApplyMigrations();
}
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

//Sercurity
void RegisterServicesForSecurity(ConfigurationManager configuration, IServiceCollection services)
{
    //Identity
    services.AddIdentity<Infrastructure.Repositories.SQLServer.DataContext.User, IdentityRole<Guid>>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    services.AddAuthorization();
    
    services.AddAuthentication(options => 
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["JWTKey:ValidAudience"],
            ValidIssuer = configuration["JWTKey:ValidIssuer"],
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTKey:Secret"] ?? throw new Exception("JWT configuration is missing")))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["jwt"];
                return Task.CompletedTask;
            }
        };
    });
}


//
void RegisterServicesForApp(ConfigurationManager configuration, IServiceCollection services)
{


    var repositoryOptions = configuration.GetSection("Repository").Get<RepositoryOptions>() ?? throw new Exception("No RepositoryOptions Found"); 

    if(repositoryOptions.Type == RepositoryType.SQLServer)
    {
        services.AddAutoMapper(typeof(MapperProfile));
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("EngExamConnection"));
            //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        services.AddTransient<IQuestionRepository>(service => new QuestionRepository(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));

        services.AddTransient<IExamRepository>(service => new ExamRepository(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));

        services.AddTransient<IQuestionManager>(service => new QuestionManager(
            service.GetRequiredService<IUnitOfWork>()));

        services.AddTransient<IAnswerRepository>(service => new AnswerRepository(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));

        services.AddTransient<IExamResultRepository>(service => new ExamResultRepository(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));
        services.AddTransient<IExamCategoryRepository>(service => new ExamCategoryRepository(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));

        services.AddTransient<IUnitOfWork>(service => new UnitOfWork(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));

        services.AddTransient<IRoleServices>(service => new RoleServices(
            service.GetRequiredService<RoleManager<IdentityRole<Guid>>>()));
        
    }

    
    //cache
    var cacheOptions = configuration.GetSection("CacheSetting").Get<CacheOptions>() ?? new CacheOptions();
    InitializeCache(configuration, services, cacheOptions);


    //handler
    services.AddSingleton<MultipleChoiceHandler>();
    services.AddSingleton<FillInBlankHandler>();
    services.AddSingleton<IDictionary<QuestionTypes, IQuestionTypesHandler>>(services => new Dictionary<QuestionTypes, IQuestionTypesHandler>
        {
            { QuestionTypes.MultipleChoice, services.GetRequiredService<MultipleChoiceHandler>() },
            { QuestionTypes.FillInTheBlank, services.GetRequiredService<FillInBlankHandler>()}
        });


    //usecase
    services.AddTransient<ISubmitExam>(service => new SubmitExam(
        service.GetRequiredService<IUnitOfWork>(),
        service.GetRequiredService<IGetExamFinder>(),
        service.GetRequiredService<IDictionary<QuestionTypes, IQuestionTypesHandler>>()));
    services.AddTransient<IGetRandomExam>(service => new CachableRandomExam(
        service.GetRequiredService<ILogger<CachableRandomExam>>(),
        new GetRandomExam(service.GetRequiredService<IUnitOfWork>()),
        service.GetRequiredService<IDistributedCache>(),
        configuration.GetSection("CachableRandomExam").Get<CachableRandomExamOptions>() ?? new()
        ));
    services.AddTransient<IGetExamFinder>(service => new CachableExamFinder(
        new RepositoryExamFinder(service.GetRequiredService<IExamRepository>()),
        service.GetRequiredService<IDistributedCache>(),
        configuration.GetSection("CachableExamById").Get<CachableExamFinderOptions>() ?? new(),
        service.GetRequiredService<ILogger<CachableExamFinder>>()
        ));
    var aiOption = configuration.GetSection("AIModel").Get<AIOptions>() ?? new AIOptions();
    services.AddTransient<IAISupport>(services => new OpenAISupport(
        services.GetRequiredService<IChatClient>()));
    RegisterAIServices(configuration, services, aiOption);
    services.AddTransient<IAIChatBox>(service => new AIChatBox(
        service.GetRequiredService<IAISupport>()
        ));
    services.AddTransient<IAuthService>(services => new AuthenService(
        services.GetRequiredService<UserManager<Infrastructure.Repositories.SQLServer.DataContext.User>>(),
        services.GetRequiredService<SignInManager<Infrastructure.Repositories.SQLServer.DataContext.User>>(),
        services.GetRequiredService<RoleManager<IdentityRole<Guid>>>(),
        services.GetRequiredService<IMapper>(),
        services.GetRequiredService<IConfiguration>()
        ));
    services.AddTransient<IRegisterAccount>(services => new RegisterAccount(
        services.GetRequiredService<IAuthService>(),
        services.GetRequiredService<IRoleServices>()
        ));
    services.AddTransient<ILoginAccount>(services => new LoginAccount(
        services.GetRequiredService<IAuthService>()
        ));
    services.AddTransient<IGetExamCategory>(service => new GetExamCategory(
        service.GetRequiredService<IExamCategoryRepository>()
        ));
    services.AddTransient<ICreateNormalExam>(service => new CreateNormalExam(
        service.GetRequiredService<IUnitOfWork>()
        ));
}
void RegisterAIServices(ConfigurationManager configuration, IServiceCollection services, AIOptions aiOption)
{
    switch (aiOption.ModelType)
    {
        case AIModel.OpenAI:
            if(aiOption.OpenAIOptions == null)
            {
                throw new Exception("OpenAIOptions is not configured.");
            }
            services.AddChatClient(new OpenAI.Chat.ChatClient(aiOption.OpenAIOptions.ModelOptions, aiOption.OpenAIOptions.API_Key).AsIChatClient());

            break;
    }
}

void InitializeCache(ConfigurationManager configuration, IServiceCollection services, CacheOptions cacheOptions)
{
    switch(cacheOptions.CacheType)
    {
        case CacheType.Memory:
            services.AddMemoryCache();
            break;
        case CacheType.Redis:
            if(cacheOptions.RedisOptions == null)
            {
                throw new Exception("RedisOptions is not configured.");
            }
            services.AddStackExchangeRedisCache(options => {
                options.Configuration = configuration.GetConnectionString(cacheOptions.RedisOptions.ConnectionStringName);

            });
            break;
        default:
            throw new NotSupportedException($"Cache type {cacheOptions.CacheType} is not supported.");
    }
}