using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExamCategoryController : ControllerBase
    {
        private readonly IGetExamCategory _getExamCategory;
        public ExamCategoryController(IGetExamCategory getExamCategory)
        {
            _getExamCategory = getExamCategory;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getExamCategory.GetAllExamCategoryAsync();
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
