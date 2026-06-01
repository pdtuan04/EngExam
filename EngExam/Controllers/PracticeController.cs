using Application.Common.Constants;
using Application.Common.Interfaces;
using Application.Features.Practice.Commands;
using Application.Features.Practice.Queries;
using Application.Models.Practice;
using Microsoft.AspNetCore.Authorization;
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
            if(result == null)
                return NotFound(new
                {
                    success = false,
                    message = "Get practice failed",
                });
            return Ok(new
            {
                success = true,
                message = "Get practice successfully",
                data = result
            });
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreatePractice([FromBody] CreatePracticeRequest request)
        {
            var command = new AddPracticeCommand(request.Title, request.TopicId, request.Description, request.Questions);
            var result = await Sender.Send(command);
            if (result == null)
                return NotFound(new
                {
                    success = false,
                    message = "Create practice failed",
                });
            return Ok(new
            {
                success = true,
                message = "Create practice successfully",
                data = result
            });
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePractice(Guid id, [FromBody] UpdatePracticeRequest request)
        {
            var command = new UpdatePracticeCommand(
                                                    id, 
                                                    request.Title, 
                                                    request.TopicId, 
                                                    request.Description, 
                                                    request.IsActive, 
                                                    request.Questions);
            var result = await Sender.Send(command);
            if (result == null)
                return NotFound(new
                {
                    success = false,
                    message = "Update practice failed",
                });
            return Ok(new
            {
                success = true,
                message = "Update practice successfully",
                data = result
            });
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePractice(Guid id)
        {
            var command = new DeletePracticeCommand(id);
            var result = await Sender.Send(command);
            if (!result)
                return NotFound(new
                {
                    success = false,
                    message = "Delete practice failed",
                });
            return Ok(new
            {
                success = true,
                message = "Delete practice successfully",
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
