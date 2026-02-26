using Application.Common.Interfaces;
using Application.Models.Exam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers.Admin
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExamManagementController : ControllerBase
    {
        private readonly IExamService _examService;
        public ExamManagementController(IExamService examService)
        {
            _examService = examService ?? throw new ArgumentNullException(nameof(examService));
        }
        [HttpPost]
        public async Task <IActionResult> CreateExam(CreateExamRequest request)
        {
            var result = await _examService.Create(request);
            if(result == null)
                return NotFound(new
                {
                    success = false,
                    message = "Create exam failed",
                });
            return Ok(new
            {
                success = true,
                message = "Create exam successfully",
                data = result
            });
        }
    }
}
