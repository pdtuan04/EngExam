using Application.Features.Exam.Queries;
using Application.Features.Topic.Queries;
using Application.Models.Pagination;
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
    }
}
