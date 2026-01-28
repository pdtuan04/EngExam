using Application.DTOs.Responses;
using Application.Interface.Identity;
using AutoMapper;
using Azure;
using Azure.Core;
using Domain.Entity;
using Google.Apis.Auth;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.Services
{
    public class AuthenService : IAuthService
    {
        private readonly UserManager<Repositories.SQLServer.DataContext.User> _userManager;
        private readonly SignInManager<Repositories.SQLServer.DataContext.User> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AuthenService(
            UserManager<Repositories.SQLServer.DataContext.User> userManager,
            SignInManager<Repositories.SQLServer.DataContext.User> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IMapper mapper,
            IConfiguration configuration)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task<bool> CheckUserExist(string userName, string email)
        {
            var userByEmail = await _userManager.FindByEmailAsync(email);
            if (userByEmail != null) return true;
            var userByName = await _userManager.FindByNameAsync(userName);
            if (userByName != null) return true;
            return false;
        }

        public async Task<bool> Register(Domain.Entity.User request)
        {
            var newUser = new Repositories.SQLServer.DataContext.User
            {
                UserName = request.UserName,
                Age = request.Age ?? 0,
                Email = request.Email,
            };
            var result = await _userManager.CreateAsync(newUser, request.Password);
            return result.Succeeded;
        }
        public async Task<LoginResponse> Login(string username, string password, bool rememberme)
        {
            var userByName = await _userManager.FindByNameAsync(username);

            if (userByName == null) throw new Exception("Ko tim thay");

            var result = await _userManager.CheckPasswordAsync(userByName, password);
            if (!result) throw new Exception("Sai tai khoan hoac mat khau");
            var token = await JwtTokenGen(_mapper.Map<Domain.Entity.User>(userByName));
            var response = new LoginResponse { Token = token, UserId = userByName.Id.ToString() };
            return response;
        }
        public async Task<string> JwtTokenGen(Domain.Entity.User user)
        {
            var userRoles = await _userManager.GetRolesAsync(_mapper.Map<Repositories.SQLServer.DataContext.User>(user));
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            };
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var token = GenerateToken(authClaims);
            return token;
        }
        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWTKey:ValidIssuer"],
                Audience = _configuration["JWTKey:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["JWTKey:TokenExpiryTimeInHour"])),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new Exception("User not found");
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded) return true;
            return false;
        }
        public async Task<LoginResponse> LoginByGoogleAsync(string idToken)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
            var userByName = await _userManager.FindByEmailAsync(payload.Email);

            if (userByName == null)
            {
                var newUser = new Repositories.SQLServer.DataContext.User
                {
                    UserName = payload.Email,
                    Email = payload.Email,
                };
                var result = await _userManager.CreateAsync(newUser);
                userByName = newUser;
            }
            var token = await JwtTokenGen(_mapper.Map<Domain.Entity.User>(userByName));
            var response = new LoginResponse { Token = token, UserId = userByName.Id.ToString() };
            return response;
        }
    }
}
