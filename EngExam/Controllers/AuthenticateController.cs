using Application.DTOs.Requests.Account;
using Application.Interface.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public AuthenticateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("register-account")]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterAccountDefaultRequest request, [FromServices] IRegisterAccount registerAccount)
        {
            var result = await registerAccount.RegisterAccount_Default(request);
            if(!result)
            {
                return BadRequest("Authentication failed");
            }
            return Ok("Authentication successful");
        }
        [HttpPost("login-account")]
        public async Task<IActionResult> LoginAccount([FromBody] LoginAccountDefaultRequest request, [FromServices] ILoginAccount loginAccount)
        {
            var result = await loginAccount.LoginAccount_Default(request);
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
        public async Task<IActionResult> LoginByGoogle([FromServices] ILoginAccountByGoogle loginAccount,[FromBody] string idToken)
        {
            var result = await loginAccount.LoginByGoogle(idToken);
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
