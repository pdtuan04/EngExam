using Application.Features.User.Commands;
using Application.Models.Authen;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ApiController
    {
        private readonly IConfiguration _configuration;

        public AuthenticateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("register-account")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var command = new SignUpCommand(request.UserName, request.Email, request.Password, request.ConfirmPassword, request.Age);
            var result = await Sender.Send(command);
            if (!result)
            {
                return BadRequest("Authentication failed");
            }
            return Ok("Authentication successful");
        }
        [HttpPost("login-account")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            var command = new SignInCommand(request.UserName, request.Password, request.RememberMe);
            var result = await Sender.Send(command);
            Response.Cookies.Append("jwt", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["JWTKey:TokenExpiryTimeInHour"]))
            });
            return Ok(new
            {
                success = true,
                data = result,
                message = "Login successful"
            });
        }
        [HttpPost("login-google")]
        public async Task<IActionResult> LoginByGoogle([FromBody] string idToken)
        {
            var command = new SignInByGoogleCommand(idToken);
            var result = await Sender.Send(command);
            Response.Cookies.Append("jwt", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["JWTKey:TokenExpiryTimeInHour"]))
            });
            return Ok(new
            {
                success = true,
                data = result,
                message = "Login successful"
            });
        }
        [HttpPost("change-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword([FromBody] ForgotPasswordRequest request)
        {
            var emailFromToken = ClaimsExtensions.GetUserEmail(User);
            var targetEmail = !string.IsNullOrEmpty(emailFromToken) ? emailFromToken : request.Email;
            if (string.IsNullOrEmpty(targetEmail))
            {
                return BadRequest("Please provide an email address.");
            }
            var command = new ForgotPasswordCommand(request.Email);
            var result = await Sender.Send(command);
            return Ok(result);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var command = new ResetPasswordCommand(request.Email, request.ResetCode, request.NewPassword);
            var result = await Sender.Send(command);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok("Logout successful");
        }
    }
}
