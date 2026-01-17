using Application.DTOs.Requests.Exam;
using Application.Interface;
using EngExam.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Authorize]
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
                return NotFound("No exam found");
            }
            return Ok(exam);
        }
        [HttpPost("submit-exam")]
        public async Task<IActionResult> SubmitExam([FromBody] SubmitExamRequest submitExam)
        {
            var userId = ClaimsExtensions.GetUserId(User);
            var result = await _submitExam.SubmitExamAsync(submitExam, userId);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var exam = await _getExam.GetExamByIdAsync(id);
            if (exam == null)
            {
                return NotFound($"Exam with ID {id} not found.");
            }
            return Ok(exam);
        }
        [HttpGet("exam-list-{id}")]
        public async Task<IActionResult> GetByIdCategory(Guid id)
        {
            var exam = await _getExam.GetExamByIdAsync(id);
            if (exam == null)
            {
                return NotFound($"Exam with ID {id} not found.");
            }
            return Ok(exam);
        }
    }
}
