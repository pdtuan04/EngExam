using Application.Features.Course.Command;
using Application.Features.Course.Commands;
using Application.Features.Course.Queries;
using Application.Models.Course;
using Application.Models.Pagination;
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
            var result = await Sender.Send(query, cancellationToken);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseRequest request, CancellationToken cancellationToken)
        {
            var command = new AddCourseCommand(request.Name, request.Description, request.Content, request.ImageUrl, request.TopicId);
            var result = await Sender.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(Guid id, UpdateCourseRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateCourseCommand(id, request.Name, request.Description, request.Content, request.ImageUrl, request.TopicId);
            var result = await Sender.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteCourseCommand(id);
            var result = await Sender.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCourses([FromQuery] PaginatedRequest paginatedRequest)
        {
            var query = new GetCoursesPaginatedQuery(paginatedRequest.PageIndex, paginatedRequest.PageSize);
            var result = await Sender.Send(query);
            return Ok(result);
        }
        [HttpGet("cicd")]
        public async Task<IActionResult> TestCICD()
        {
            //return Ok("ok");
            return Ok(new
            {
                success = true,
                data = "CICD pipeline is working fine",
                message = "CICD pipeline test successful"
            });
        }
    }
}
