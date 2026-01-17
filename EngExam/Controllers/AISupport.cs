using Application;
using Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AISupport : ControllerBase
    {
        public readonly IAIChatBox _aIChatBox;
        public AISupport(IAIChatBox aIChatBox)
        {
            _aIChatBox = aIChatBox;
        }
        [HttpGet("get-response")]
        public async Task<IActionResult> GetAIResponse([FromQuery] string prompt)
        {
            var response = await _aIChatBox.GetAIResponseAsync(prompt);
            return Ok(response);
        }
    }
}
