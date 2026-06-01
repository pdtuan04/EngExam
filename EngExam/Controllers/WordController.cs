using Application.Common.Interfaces;
using Application.Features.Word.Commands;
using Application.Features.Word.Events;
using Application.Features.Word.Queries;
using Application.Models.Word;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ApiController
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateWord([FromBody] CreateWordRequest request)
        {
            var command = new CreateWordCommand(request.Text, request.Meaning, request.FlashCardId);
            var createdWord = await Sender.Send(command);
            return Ok(createdWord);
        }
        [HttpGet("meaning")]
        public async Task<IActionResult> GetWordMeaning([FromQuery] string text)
        {
            var query = new GetWordMeaningQuery(text);
            var meaning = await Sender.Send(query);
            return Ok(meaning);
        }
        [Authorize]
        [HttpPatch("{id}/memorized")]
        public async Task<IActionResult> MarkWordAsMemorized(Guid id, [FromBody] SetWordMemorizationStatusRequest request)
        {

            var command = new ToggleWordMemorizationCommand(id, request.IsMemorized, request.FlashCardId);
            var status = await Sender.Send(command);
            return Ok(new
            {
                isMemorized = status,
                success = true,
            });
        }
    }
}
