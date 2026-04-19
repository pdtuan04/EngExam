using Application.Common.Interfaces;
using Application.Features.ExamCategory.Commands;
using Application.Features.ExamCategory.Queries;
using Application.Models.ExamCategory;
using Application.Models.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamCategoryController : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExamCategoryRequest request)
        {
            var command = new CreateExamCategoryCommand(request.Name, request.Description, request.ImageUrl);
            var result = await Sender.Send(command);
            if (result == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Failed to create exam category"
                });
            }
            return Ok(new
            {
                success = true,
                data = result,
                message = "Exam category created successfully"
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCategoryQuery();
            var result = await Sender.Send(query);
            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "No exam categories found"
                });
            }
            return Ok(new
            {
                success = true,
                data = result,
                message = "Exam categories retrieved successfully"
            });
        }
        [AllowAnonymous]
        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] PaginatedRequest request)
        {
            var query = new GetExamCategoryPaginatedQuery(request.PageIndex, request.PageSize);
            var result = await Sender.Send(query);
            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "No exam categories found"
                });
            }
            return Ok(new
            {
                success = true,
                data = result,
                message = "Exam categories retrieved successfully"
            });
        }

    }
}
