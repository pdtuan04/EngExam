using Application.Features.FlashCard.Commands;
using Application.Features.FlashCard.Queries;
using Application.Models.FlashCard;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashCardController : ApiController
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetFlashCards()
        {
            var userId = ClaimsExtensions.GetUserId(User);
            var query = new GetFlashCardByUserIdQuery(userId);
            var result = await Sender.Send(query);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateFlashCard([FromBody] CreateFlashCardRequest request)
        {
            var userId = ClaimsExtensions.GetUserId(User);
            var command = new CreateFlashCardCommand(request.Title, request.Description, userId);
            var result = await Sender.Send(command);
            return Ok(result);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateFlashCard([FromBody] UpdateFlashCardRequest request)
        {
            var userId = ClaimsExtensions.GetUserId(User);
            var command = new UpdateFlashCardCommand(request.Id, request.Title, request.Description);
            var result = await Sender.Send(command);
            return Ok(result);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlashCard(Guid id)
        {
            var userId = ClaimsExtensions.GetUserId(User);
            var command = new DeleteFlashCardCommand(id);
            var result = await Sender.Send(command);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlashCardById(Guid id)
        {
            var query = new GetFlashCardDetailByIdQuery(id);
            var result = await Sender.Send(query);
            return Ok(result);
        }
    }
}
 