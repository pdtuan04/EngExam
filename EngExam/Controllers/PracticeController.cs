using Application.Common.Interfaces;
using Application.Features.Practice.Queries;
using Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeController : ApiController
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPracticeToTake(Guid id)
        {
            var query = new GetPracticeToTakeQuery(id);
            var result = await Sender.Send(query);
            return Ok(new
            {
                success = true,
                message = "Get practice successfully",
                data = result
            });
        }

    }
}
