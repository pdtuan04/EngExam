using Application.Common.Constants;
using Application.Features.User.Commands;
using Application.Features.User.Queries;
using Application.Models.User;
using Domain.Enums;
using Infrastructure.Extensions;
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
        [Authorize(Roles = Roles.Admin)]
        [HttpGet("count-by-month")]
        public async Task<IActionResult> GetCreatedUserCountByMonth([FromQuery] int year, [FromQuery] int month, CancellationToken cancellationToken)
        {
            var count = await Sender.Send(new GetCreatedUserCountByMonthQuery(year, month), cancellationToken);
            return Ok(count);
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpGet("count-by-year")]
        public async Task<IActionResult> GetCreatedUserCountByYear([FromQuery] int year, CancellationToken cancellationToken)
        {
            var count = await Sender.Send(new GetCreatedUserCountByYearQuery(year), cancellationToken);
            return Ok(count);
        }
    }
}
