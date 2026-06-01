using Application.Common.Constants;
using Application.Features.Exam.Queries;
using Application.Features.Topic.Commands;
using Application.Features.Topic.Queries;
using Application.Models.Pagination;
using Application.Models.Topic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ApiController
    {
        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] PaginatedRequest request)
        {
            var query = new GetTopicPaginatedQuery(request.PageIndex, request.PageSize);
            var result = await Sender.Send(query);
            return Ok(new
            {
                success = true,
                data = result,
                message = "Get paginated topics successfully"
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTopic(CancellationToken cancellationToken)
        {
            var query = new GetAllTopicQuery();
            var result = await Sender.Send(query, cancellationToken);
            return Ok(new
            {
                success = true,
                data = result,
                message = "Get topic by id successfully"
            });
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromBody]CreateTopicRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateTopicCommand(request.Name, request.Description);
            var result = await Sender.Send(command, cancellationToken);
            return Ok(new
            {
                success = true,
                data = result,
                message = "Create topic successfully"
            });
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTopic(Guid id, [FromBody] UpdateTopicRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateTopicCommand(id, request.Name, request.Description);
            var result = await Sender.Send(command, cancellationToken);
            return Ok(new
            {
                success = true,
                data = result,
                message = "Update topic successfully"
            });
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopic(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteTopicCommand(id);
            var result = await Sender.Send(command, cancellationToken);
            return Ok(new
            {
                success = result,
                message = "Delete topic successfully"
            });
        }
    }
}
