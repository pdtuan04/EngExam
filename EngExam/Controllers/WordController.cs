using Application.Common.Interfaces;
using Application.Features.Word.Commands;
using Application.Features.Word.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ApiController
    {
        private readonly ITranslateService _translateService;
        public WordController(ITranslateService translateService)
        {
            _translateService = translateService;
        }
        [HttpPost("translate")]
        public async Task<IActionResult> Translate([FromBody] string text)
        {
            var query = new GetWordQuery(text);
            var result = await Sender.Send(query);
            if (result != null) return Ok(result);
            var translations = await _translateService.TranslateAsync(text);
            var command = new CreateWordCommand(text, translations);
            var createdWord = await Sender.Send(command);
            return Ok(createdWord);
        }
    }
}
