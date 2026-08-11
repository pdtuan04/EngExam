using Application.Features.Comment.Commands;
using Application.Features.Comment.Queries;
using Application.Models.Comment;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ApiController
    {
        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetPostComments(Guid courseId, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 5)
        {
            var query = new GetCourseCommentQuery(courseId, pageIndex, pageSize);
            var result = await Sender.Send(query);
            return Ok(result);
        }
        [HttpGet("{parentId}/replies")]
        public async Task<IActionResult> GetCommentReplies(Guid parentId, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 5)
        {
            var query = new GetCommentReplyQuery(parentId, pageIndex, pageSize);
            var result = await Sender.Send(query);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request)
        {
            var userId = ClaimsExtensions.GetUserId(User);
            var command = new AddCommentCommand(request.parentId, request.content, request.courseId, userId);
            var result = await Sender.Send(command);
            return Ok(result);
        }
    }
}