using Application.DTOs;
using Application.DTOs.Requests.Exam;
using Application.Interface.Exam;
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
        private readonly IGetRandomExam _getRandomExam;
        private readonly IGetExamFinder _getExam;
        private readonly ISubmitExam _submitExam;
        private readonly IGetPaginatedExam _paginatedExam;
        public ExamController(IGetRandomExam getRandomExam, ISubmitExam submitExam, IGetExamFinder getExam, IGetPaginatedExam paginatedExam)
        {
            _getRandomExam = getRandomExam;
            _submitExam = submitExam;
            _getExam = getExam;
            _paginatedExam = paginatedExam;
        }
        [HttpGet("random")]
        public async Task<IActionResult> GetRandomExam()
        {
            var exam = await _getRandomExam.GetRandomExamAsync();
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
            var result = await _submitExam.SubmitExamAsync(submitExam, userId);
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
            var exam = await _getExam.GetExamForDoingAsync(id);
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
            var exam = await _getExam.GetExamsByCategoryIdAsync(id);
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
        public async Task<IActionResult> GetPaginatedExams([FromQuery] PaginatedDTO request)
        {
            var exams = await _paginatedExam.GetPaginatedExamsAsync(request);
            return Ok(new
            {
                success = true,
                data = exams,
                message = "Get paginated exams successfully"
            });
        }
    }
}
