using Application.DTOs.Requests.Exam;
using Application.Interface;
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
        public ExamController(IGetRandomExam getRandomExam, ISubmitExam submitExam, IGetExamFinder getExam)
        {
            _getRandomExam = getRandomExam;
            _submitExam = submitExam;
            _getExam = getExam;
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
    }
}
