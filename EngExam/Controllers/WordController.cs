using Application.Common.Interfaces;
using Application.Features.Word.Commands;
using Application.Features.Word.Queries;
using Application.Models.Word;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateWord([FromBody] CreateWordRequest request)
        {
            var command = new CreateWordCommand(request.Text, request.Meaning);
            var createdWord = await Sender.Send(command);
            return Ok(createdWord);
        }
        [HttpGet("meaning")]
        public async Task<IActionResult> GetWordMeaning([FromQuery]string text)
        {
            var query = new GetWordMeaningQuery(text);
            var meaning = await Sender.Send(query);
            return Ok(meaning);
        }
    }
}
