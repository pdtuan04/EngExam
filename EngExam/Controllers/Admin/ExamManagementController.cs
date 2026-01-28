using Application.DTOs.Exam.Management;
using Application.Interface.Exam;
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
        private readonly ICreateNormalExam _createNormalExam;
        public ExamManagementController(ICreateNormalExam createNormalExam)
        {
            _createNormalExam = createNormalExam ?? throw new ArgumentNullException();
        }
        [HttpPost]
        public async Task <IActionResult> CreateExam(CreateExamDTO request)
        {
            var result = await _createNormalExam.CreateExam(request);
            if(result == Guid.Empty)
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
