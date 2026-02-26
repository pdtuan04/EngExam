using Application.Common.Interfaces;
using Application.Models.Exam;
using Application.Models.Pagination;
using EngExam.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;
        public ExamController(IExamService examService)
        {
            _examService = examService;
        }
        [HttpGet("random")]
        public async Task<IActionResult> GetRandomExam()
        {
            var exam = await _examService.GetRandomExamToTake();
            if (exam == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "No exam found"
                });
            }
            return Ok(new
            {
                success = true,
                data = exam,
                message = "Random exam retrieved successfully"
            });
        }
        [Authorize]
        [HttpPost("submit-exam")]
        public async Task<IActionResult> SubmitExam([FromBody] SubmitExamRequest submitExam)
        {
            var userId = ClaimsExtensions.GetUserId(User);
            var result = await _examService.SubmitExam(userId, submitExam);
            return Ok(new
            {
                success = true,
                data = result,
                message = "Submit exam successfully"
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamById(Guid id)
        {
            var exam = await _examService.GetExamToTake(id);
            if (exam == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "No exam found"
                });
            }
            return Ok(new
            {
                success = true,
                data = exam,
                message = "Get exam by id successfully"
            });
        }
        [HttpGet("exam-list-{id}")]
        public async Task<IActionResult> GetExamByIdCategory(Guid id)
        {
            var exam = await _examService.GetExamsByCategoryIdAsync(id);
            if (exam == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "No exam found"
                });
            }
            return Ok(new
            {
                success = true,
                data = exam,
                message = "Get exam by category successfully"
            });
        }
        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] PaginatedRequest request)
        {
            var exams = await _examService.GetPaginated(request);
            return Ok(new
            {
                success = true,
                data = exams,
                message = "Get paginated exams successfully"
            });
        }
    }
}
