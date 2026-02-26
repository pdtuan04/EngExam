using Application.Common.Interfaces;
using Application.Models.Authen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        public AuthenticateController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }
        [HttpPost("register-account")]
        public async Task<IActionResult> SignUp([FromBody]SignUpRequest request)
        {
            var result = await _authService.SignUp(request);
            if(!result)
            {
                return BadRequest("Authentication failed");
            }
            return Ok("Authentication successful");
        }
        [HttpPost("login-account")]
        public async Task<IActionResult> LoginAccount([FromBody] SignInRequest request)
        {
            var result = await _authService.SignIn(request.UserName, request.Password, request.RememberMe);
            //var token = 
            Response.Cookies.Append("jwt", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["JWTKey:TokenExpiryTimeInHour"]))
            });
            return Ok("Login successful");
        }
        [HttpPost("login-google")]
        public async Task<IActionResult> LoginByGoogle([FromBody] string idToken)
        {
            var result = await _authService.LoginByGoogle(idToken);
            //var token = 
            Response.Cookies.Append("jwt", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["JWTKey:TokenExpiryTimeInHour"]))
            });
            return Ok("Login successful");
        }
    }
}
