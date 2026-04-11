using Application.Features.Course.Command;
using Application.Features.Course.Queries;
using Application.Models.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetCourseByIdQuery(id);
            var result = await Sender.Send(query);
            return Ok(result);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> CreateCourse(CreateCourseRequest request, CancellationToken cancellationToken)
        {
            var command = new AddCourseCommand(request.Name,request.Description, request.Content, request.ImageUrl, request.TopicId);
            var result = await Sender.Send(command);
            return Ok(result);
        }
    }
}
