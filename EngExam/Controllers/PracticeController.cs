using Application.Common.Interfaces;
using Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeController : ControllerBase
    {
        private readonly IPracticeService _practiceService;
        public PracticeController(IPracticeService practiceService)
        {
            _practiceService = practiceService ?? throw new ArgumentNullException(nameof(practiceService));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPracticeToTake(Guid id)
        {
            var result = await _practiceService.GetPracticeToTake(id);
            return Ok(new
            {
                success = true,
                message = "Get practice successfully",
                data = result
            });
        }

    }
}
