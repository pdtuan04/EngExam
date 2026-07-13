using Application.Features.User.Commands;
using Application.Features.User.Queries;
using Application.Models.User;
using EngExam.Extensions;
using MassTransit.Futures.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiController
    {
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetUsers()
        {
            var userId = ClaimsExtensions.GetUserId(User);
            var query = new GetUserByIdQuery(userId);
            var result = await Sender.Send(query);
            if(result == null)
                return NotFound();
            return Ok(result);
        }
        [Authorize]
        [HttpPatch("update-avatar")]
        public async Task<IActionResult> UpdateUserAvatar([FromBody] ChangeAvatarRequest request)
        {
            var userId = ClaimsExtensions.GetUserId(User);
            var command = new UpdateAvatarCommand(userId, request.AvatarUrl);
            var result = await Sender.Send(command);
            return Ok(result);
        }
    }
}
