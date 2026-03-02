using Application.Common.Interfaces;
using Application.Models.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamCategoryController : ControllerBase
    {
        private readonly IExamCategoryService _examCategoryService;
        public ExamCategoryController(IExamCategoryService examCategoryService)
        {
            _examCategoryService = examCategoryService ?? throw new ArgumentNullException(nameof(examCategoryService));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _examCategoryService.GetAll();
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
            var result = await _examCategoryService.GetPaginated(request);
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
