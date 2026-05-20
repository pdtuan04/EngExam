using Amazon;
using Amazon.S3;
using Application;
using Application.Abstractions;
using Application.Abstractions.Caching;
using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Behaviors;
using Application.Common.Interfaces;
using Application.Exceptions;
using Application.Features.Course.Consumers;
using Application.Features.Exam.Consumers;
using Application.Features.ExamCategory.Consumers;
using Application.Features.ExamResult.Consumers;
using Application.Features.FlashCard.Consumers;
using Application.Features.Practice.Consumer;
using Application.Features.Topic.Consumers;
using Application.Features.Word.Consumers;
using Application.Handler;
using Application.Handler.InterfaceHandler;
using AutoMapper;
using Domain.Enums;
using EngExam.Extensions;
using EngExam.Middlewares;
using EngExam.OptionsModels;
using Hangfire;
using Hangfire.SqlServer;
using Infrastructure;
using Infrastructure.Authentication;
using Infrastructure.Cache;
using Infrastructure.Common;
using Infrastructure.Email;
using Infrastructure.Events;
using Infrastructure.File;
using Infrastructure.FileServices;
using Infrastructure.Realtime;
using Infrastructure.Repositories.SQLServer;
using Infrastructure.Repositories.SQLServer.DataContext;
using Infrastructure.Repositories.SQLServer.Mappers;
using Infrastructure.Repositories.SQLServer_Read;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;
//    /\_____/\
//   / o   o   \
//  (==  ^    ==)
//   )         (
//  (           )
// ( (  )   (  ) )
//(__(__)___(__)__)
//Phan Dinh Tuan - HUTECH University
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:5175");
                          //policy.AllowAnyOrigin();
                          policy.AllowAnyHeader();
                          policy.AllowCredentials();
                          policy.AllowAnyMethod();
                      });
});
var externalAuthOptions = builder.Configuration.GetSection("ExternalAuth").Get<ExternalAuthOptions>() ?? new ExternalAuthOptions();
RegisterServicesForSecurity(builder.Configuration, builder.Services, externalAuthOptions);
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
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
RegisterServicesForApp(builder.Configuration, builder.Services);


builder.Services.AddHttpClient();
var app = builder.Build();
app.UseStaticFiles();
// Configure the HTTP request pipeline.
app.UseCors(MyAllowSpecificOrigins);
app.UseApplyMigrations();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "api");
    });
}
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<OnlineCounter>("/onlineCounter");
app.MapControllers();

app.Run();

//Sercurity
void RegisterServicesForSecurity(ConfigurationManager configuration, IServiceCollection services, ExternalAuthOptions externalAuthOptions)
{
    //Identity
    switch(externalAuthOptions.ExternalAuthTypes)
    {
        case ExternalAuthTypes.Google:
            if(externalAuthOptions.GoogleAuthOptions == null)
            {
                throw new Exception("GoogleAuthOptions is not configured.");
            }

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
            }).AddGoogle(options =>
            {
                options.ClientId = externalAuthOptions.GoogleAuthOptions.ClientId;
                options.ClientSecret = externalAuthOptions.GoogleAuthOptions.ClientSecret;
            });
            break;
        default:
            throw new NotImplementedException();
    }
}


//
void RegisterServicesForApp(ConfigurationManager configuration, IServiceCollection services)
{


    var repositoryOptions = configuration.GetSection("Repository").Get<RepositoryOptions>() ?? throw new Exception("No RepositoryOptions Found");

    if (repositoryOptions.Type == RepositoryType.SQLServer)
    {
        //services.AddAutoMapper(typeof(MapperProfile));
        services.AddAutoMapper(cfg => { }, typeof(MapperProfile));
        //Write Database side
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("EngExamConnection"));
            //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        //email
        builder.Services.Configure<EmailOptions>(configuration.GetSection("EmailSetting"));
        services.AddTransient<IEmailService>(services => new SMTP(
            services.GetRequiredService<IOptions<EmailOptions>>()
            ));
        //hangfire
        builder.Services.AddHangfire(config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(builder.Configuration.GetConnectionString("EngExamConnection")));
        builder.Services.AddHangfireServer();

        services.AddTransient<IQuestionRepository>(service => new QuestionRepository(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));

        services.AddTransient<IExamRepository>(service => new ExamRepository(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));

        services.AddTransient<IAnswerRepository>(service => new AnswerRepository(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));

        services.AddTransient<IExamResultRepository>(service => new ExamResultRepository(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));
        services.AddTransient<IExamCategoryRepository>(service => new ExamCategoryRepository(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));
        services.AddTransient<IPracticeRepository>(service => new PracticeRepository(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));
        services.AddTransient<IFlashCardRepository>(service => new FlashCardRepository(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));
        services.AddTransient<IWordRepository>(service => new WordRepository(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));
        services.AddTransient<ITopicRepository>(service => new TopicRepository(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));
        services.AddTransient<ICourseRepository>(service => new CourseRepository(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));
        services.AddScoped<IUnitOfWork>(service => new UnitOfWork(
            service.GetRequiredService<ApplicationDbContext>(),
            service.GetRequiredService<IMapper>()));
        //Read Database side
        services.AddDbContext<ApplicationDbReadContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("EngExamReadDBConnection"));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddTransient<IQuestionReadRepository>(service => new QuestionReadRepository(
            service.GetRequiredService<ApplicationDbReadContext>(),
            service.GetRequiredService<IMapper>()));

        services.AddTransient<IExamReadRepository>(service => new ExamReadRepository(
            service.GetRequiredService<ApplicationDbReadContext>(),
            service.GetRequiredService<IMapper>()));

        services.AddTransient<IAnswerReadRepository>(service => new AnswerReadRepository(
            service.GetRequiredService<ApplicationDbReadContext>(),
            service.GetRequiredService<IMapper>()));

        services.AddTransient<IExamResultReadRepository>(service => new ExamResultReadRepository(
            service.GetRequiredService<ApplicationDbReadContext>(),
            service.GetRequiredService<IMapper>()));
        services.AddTransient<IExamCategoryReadRepository>(service => new ExamCategoryReadRepository(
            service.GetRequiredService<ApplicationDbReadContext>(),
            service.GetRequiredService<IMapper>()));
        services.AddTransient<IPracticeReadRepository>(service => new PracticeReadRepository(
            service.GetRequiredService<ApplicationDbReadContext>(),
            service.GetRequiredService<IMapper>()));
        services.AddTransient<ITopicReadRepository>(service => new TopicReadRepository(
            service.GetRequiredService<ApplicationDbReadContext>(),
            service.GetRequiredService<IMapper>()));
        services.AddTransient<ICourseReadRepository>(service => new CourseReadRepository(
            service.GetRequiredService<ApplicationDbReadContext>(),
            service.GetRequiredService<IMapper>()));
        services.AddTransient<IFlashCardReadRepository>(service => new FlashCardReadRepository(
            service.GetRequiredService<ApplicationDbReadContext>(),
            service.GetRequiredService<IMapper>()));
        services.AddTransient<IWordReadRepository>(service => new WordReadRepository(
            service.GetRequiredService<ApplicationDbReadContext>(),
            service.GetRequiredService<IMapper>()));
    }

    //storage
    var storageOptions = configuration.GetSection("StorageOptions").Get<StorageOptions>() ?? new StorageOptions();
    StorageServices(configuration,services,storageOptions);
    //cache
    services.AddSingleton<ICacheService, CacheService>();
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
    services.AddTransient<IAuthIdentityService>(services => new AuthIdentityService(
        services.GetRequiredService<UserManager<Infrastructure.Repositories.SQLServer.DataContext.User>>(),
        services.GetRequiredService<SignInManager<Infrastructure.Repositories.SQLServer.DataContext.User>>(),
        services.GetRequiredService<RoleManager<IdentityRole<Guid>>>(),
        services.GetRequiredService<IMapper>(),
        services.GetRequiredService<IConfiguration>(),
        services.GetRequiredService<IEmailService>(),
        services.GetRequiredService<IBackgroundJobClient>()
        ));
    services.AddSignalR();
    services.Configure<MessageBrokerOptions>(configuration.GetSection("MessageBrokerSetting"));
    services.AddSingleton(services => services.GetRequiredService<IOptions<MessageBrokerOptions>>().Value);
    services.AddMassTransit(busConfig =>
    {
        busConfig.AddConsumer<InvalidateCourseCacheConsumer>();
        busConfig.AddConsumer<SyncCourseReadDbConsumer>();
        busConfig.AddConsumer<InvalidateExamCategoryCacheConsumer>();
        busConfig.AddConsumer<SyncExamCategoryReadDbConsumer>();
        busConfig.AddConsumer<InvalidateWordCacheConsumer>();
        busConfig.AddConsumer<SyncWordReadDbConsumer>();
        busConfig.AddConsumer<InvalidateExamCacheConsumer>();
        busConfig.AddConsumer<SyncExamReadDbConsumer>();
        busConfig.AddConsumer<InvalidateExamCategoryCacheConsumer>();
        busConfig.AddConsumer<SyncExamCategoryReadDbConsumer>();
        busConfig.AddConsumer<SyncExamResultReadDbConsumer>();
        busConfig.AddConsumer<InvalidateFlashCardCacheConsumer>();
        busConfig.AddConsumer<SyncFlashCardReadDbConsumer>();
        busConfig.AddConsumer<InvalidatePracticeCacheConsumer>();
        busConfig.AddConsumer<SyncPracticeReadDbConsumer>();
        busConfig.AddConsumer<InvalidateTopicCacheConsumer>();
        busConfig.AddConsumer<SyncTopicReadDbConsumer>();
        busConfig.AddEntityFrameworkOutbox<ApplicationDbContext>(o =>
        {
            o.UseSqlServer();
            o.UseBusOutbox(options =>
            {
                options.MessageDeliveryLimit = 100;
                options.MessageDeliveryTimeout = TimeSpan.FromSeconds(45);
                //options.ConcurrentDeliveryLimit = 10;
            });
        });
        busConfig.AddConfigureEndpointsCallback((context, name, cfg) =>
        {
            cfg.UseMessageRetry(r =>
            {
                r.Interval(5, TimeSpan.FromSeconds(3));
                r.Ignore<BusinessException>();
            });
            cfg.UseEntityFrameworkOutbox<ApplicationDbContext>(context, options =>
            {
                options.MessageDeliveryLimit = 100;
                options.MessageDeliveryTimeout = TimeSpan.FromSeconds(45);
                //options.ConcurrentDeliveryLimit = 10;
            });
        });
        busConfig.SetKebabCaseEndpointNameFormatter();
        busConfig.UsingRabbitMq((context, config) =>
        {
            MessageBrokerOptions setting = context.GetRequiredService<MessageBrokerOptions>();
            config.Host(new Uri(setting.Host), h =>
            {
                h.Username(setting.UserName);
                h.Password(setting.Password);
            });
            config.ConfigureEndpoints(context);
        });
    }); 
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
void StorageServices(ConfigurationManager configuration, IServiceCollection services, StorageOptions storageOptions)
{
    switch(storageOptions.StorageType)
    {
        case StorageType.Local:
            if(storageOptions.LocalStorageOptions == null)
            {
                throw new Exception("LocalStorageOptions is not configured.");
            }
            services.AddSingleton<IFileService, LocalStorageService>();
            break;
        case StorageType.S3:
            if(storageOptions.S3Options == null)
            {
                throw new Exception("S3Options is not configured.");
            }
            services.AddSingleton<IAmazonS3>(cg =>
            {
                var config = new AmazonS3Config
                {
                    RegionEndpoint = RegionEndpoint.GetBySystemName(storageOptions.S3Options.Region),
                };
                return new AmazonS3Client(config);
            });
            services.AddSingleton<IFileUrlResolver>(service => new CloudFrontUrlResolver(storageOptions.S3Options));

            services.AddSingleton<IFileService>(cg =>
            {
                var amazonS3 = cg.GetRequiredService<IAmazonS3>();
                return new S3StorageService(amazonS3, storageOptions.S3Options);
            });
            break;
        default:
            throw new NotSupportedException($"Storage type {storageOptions.StorageType} is not supported.");
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
            Console.WriteLine(configuration.GetConnectionString(cacheOptions.RedisOptions.ConnectionStringName));
            services.AddStackExchangeRedisCache(options => {
                options.Configuration = configuration.GetConnectionString(cacheOptions.RedisOptions.ConnectionStringName);
            });
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration.GetConnectionString(cacheOptions.RedisOptions.ConnectionStringName)));
            break;
        default:
            throw new NotSupportedException($"Cache type {cacheOptions.CacheType} is not supported.");
    }
}