using Application.Common.Interfaces;
using Application.Features.Practice.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeController : ApiController
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPracticeToTake(Guid id)
        {
            var query = new GetPracticeToTakeQuery(id);
            var result = await Sender.Send(query);
            return Ok(new
            {
                success = true,
                message = "Get practice successfully",
                data = result
            });
        }
        [HttpGet("paginated-topic")]
        public async Task<IActionResult> GetPracticePaginatedByTopicId([FromQuery] int pageIndex, int pageSize, Guid topicId)
        {
            var query = new GetPracticePaginatedByTopicIdQuery(pageIndex,pageSize,topicId);
            var result = await Sender.Send(query);  
            return Ok(new
            {
                success = true,
                message = "Get practice paginated successfully",
                data = result
            });
        }
    }
}
