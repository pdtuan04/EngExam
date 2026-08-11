using Application.Common.Constants;
using Application.Features.ExamResult.Queries;
using Application.Features.Practice.Queries;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamResultController : ApiController
    {
        [Authorize]
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetUserExamResult(Guid id)
        {
            var query = new GetExamResultDetailsQuery(id);
            var result = await Sender.Send(query);
            return Ok(new
            {
                success = true,
                message = "Get user exam result paginated successfully",
                data = result
            });
        }
        [Authorize]
        [HttpGet("paginated-user-exam-result")]
        public async Task<IActionResult> GetUserExamResult([FromQuery] int pageIndex, int pageSize)
        {
            var userId = ClaimsExtensions.GetUserId(User);
            var query = new GetExamResultPaginatedByUserIdQuery(userId, pageIndex, pageSize);
            var result = await Sender.Send(query);
            return Ok(new
            {
                success = true,
                message = "Get user exam result paginated successfully",
                data = result
            });
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpGet("count-by-month")]
        public async Task<IActionResult> GetExamResultCountByMonth([FromQuery] int year, [FromQuery] int month, CancellationToken cancellationToken)
        {
            var query = new GetCompletedExamCountByMonthQuery(year, month);
            var count = await Sender.Send(query, cancellationToken);
            return Ok(count);
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpGet("count-by-year")]
        public async Task<IActionResult> GetExamResultCountByYear([FromQuery] int year, CancellationToken cancellationToken)
        {
            var query = new GetCompletedExamCountByYearQuery(year);
            var count = await Sender.Send(query, cancellationToken);
            return Ok(count);
        }
    }
}
