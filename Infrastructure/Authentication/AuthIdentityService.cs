

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Models.Authen;
using AutoMapper;
using Google.Apis.Auth;
using Hangfire;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication
{
    public class AuthIdentityService : IAuthIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public AuthIdentityService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IMapper mapper,
            IConfiguration configuration,
            IEmailService emailService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _emailService = emailService;
        }
        public async Task<bool> CheckUserExist(string userName, string email)
        {
            var userByEmail = await _userManager.FindByEmailAsync(email);
            if (userByEmail != null) return true;
            var userByName = await _userManager.FindByNameAsync(userName);
            if (userByName != null) return true;
            return false;
        }

        public async Task<bool> SignUp(Domain.Entity.User request)
        {
            var newUser = new User
            {
                UserName = request.UserName,
                Age = request.Age ?? 0,
                Email = request.Email,
            };
            var result = await _userManager.CreateAsync(newUser, request.Password);
            return result.Succeeded;
        }
        public async Task<SignInResponse> SignIn(string username, string password, bool rememberme)
        {
            var userByName = await _userManager.FindByNameAsync(username);

            if (userByName == null) throw new Exception("Ko tim thay");

            var result = await _userManager.CheckPasswordAsync(userByName, password);
            if (!result) throw new Exception("Sai tai khoan hoac mat khau");
            var token = await JwtTokenGen(_mapper.Map<Domain.Entity.User>(userByName));
            var userRoles = await _userManager.GetRolesAsync(userByName);
            var response = new SignInResponse(token, userByName.Id, userByName.UserName ?? "", userByName.Email ?? "", userRoles.ToList(), userByName.ImageUrl);
            return response;
        }
        public async Task<string> JwtTokenGen(Domain.Entity.User user)
        {
            var userRoles = await _userManager.GetRolesAsync(_mapper.Map<User>(user));
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

        public async Task<bool> ChangePassword(Guid userId, string currentPassword, string newPassword, string confirmNewPassword)
        {
            if (newPassword != confirmNewPassword) throw new BadRequestException("New password and confirm password do not match");
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new Exception("User not found");
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded) return true;
            return false;
        }
        public async Task<ExternalSignInResult> LoginByGoogle(string idToken)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
            return new ExternalSignInResult(payload.Email);
        }

        public async Task Logout()
        {
            throw new NotImplementedException();
        }
        public async Task<string> CreateRole(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            return roleName;
        }

        public async Task<string> GetRole(string roleName)
        {
            await _roleManager.FindByNameAsync(roleName);
            return roleName;
        }
        public async Task<bool> AddUserToRole(Domain.Entity.User request, string roleName)
        {
            var user = await _userManager.FindByNameAsync(request.UserName) ?? throw new Exception("User not found");// tu tu tinh :))
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<bool> ResetPassword(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new Exception("User not found");
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded) throw new Exception("Failed to reset password");
            return true;
        }

        public async Task<Domain.Entity.User> GetByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) throw new Exception("User not found");
            return _mapper.Map<Domain.Entity.User>(user);
        }

        public async Task<string?> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<Domain.Entity.User> ChangeAvatar(Guid userId, string avatarUrl)
        {
            var now = DateTime.UtcNow;
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new NotFoundException("User", userId);
            user.ImageUrl = avatarUrl;
            user.UpdatedAt = now;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) throw new Exception("Failed to update avatar");
            return _mapper.Map<Domain.Entity.User>(user);
        }
        public async Task<IList<string>> GetUserRolesAsync(Domain.Entity.User user)
        {
            var identityUser = await _userManager.FindByNameAsync(user.UserName) ?? throw new Exception("User not found");
            return (await _userManager.GetRolesAsync(identityUser)).ToList();
        }

        public async Task<Domain.Entity.User> GetUserByEmail(string email)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            return _mapper.Map<Domain.Entity.User>(identityUser);
        }

        public async Task<bool> ExternalSignUp(Domain.Entity.User request)
        {
            var newUser = new User
            {
                Id = request.Id,
                UserName = request.UserName,
                Age = request.Age ?? 0,
                Email = request.Email,
            };
            var result = await _userManager.CreateAsync(newUser);
            return result.Succeeded;
        }
    }
}
