using Application.Common.Constants;
using Application.Common.Interfaces;
using Application.Features.Exam.Commands;
using Application.Features.Exam.Queries;
using Application.Features.ExamResult.Commands;
using Application.Models.Exam;
using Application.Models.Pagination;
using EngExam.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ApiController
    {
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateExam(CreateExamRequest request)
        {
            var command = new AddExamCommand(request.Title, request.DurationInMinutes, request.ExamCategoryId, request.Description,  request.Questions);
            var result = await Sender.Send(command);
            if (result == null)
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
        [Authorize(Roles = Roles.Admin)]
        [HttpPut]
        public async Task<IActionResult> UpdateExam([FromBody] UpdateExamRequest request)
        {
            var command = new UpdateExamCommand(request.Id, request.Title, request.DurationInMinutes, request.ExamCategoryId, request.Description, request.IsActive, request.Questions);
            var result = await Sender.Send(command);
            if (result == null)
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
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(Guid id)
        {
            var command = new DeleteExamCommand(id);
            var result = await Sender.Send(command);
            if (!result)
                return NotFound(new
                {
                    success = false,
                    message = "Delete exam failed",
                });
            return Ok(new
            {
                success = true,
                message = "Delete exam successfully",
            });
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpPatch("{id}/unactive")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var command = new DeleteExamCommand(id);
            var result = await Sender.Send(command);
            if (!result)
                return NotFound(new
                {
                    success = false,
                    message = "Change status exam failed",
                });
            return Ok(new
            {
                success = true,
                message = "Change status exam successfully",
            });
        }
        [Authorize]
        [HttpPost("submit-exam")]
        public async Task<IActionResult> SubmitExam([FromBody] SubmitExamRequest submitExam)
        {
            var userId = ClaimsExtensions.GetUserId(User);
            var command = new SaveExamResultCommand(userId, submitExam.ExamId, submitExam.UserAnswers);
            var result = await Sender.Send(command);
            return Ok(new
            {
                success = true,
                data = result,
                message = "Submit exam successfully"
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamById(Guid id)
        {
            var query = new GetExamByIdQuery(id);
            var result = await Sender.Send(query);
            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "No exam found"
                });
            }
            return Ok(new
            {
                success = true,
                data = result,
                message = "Get exam by id successfully"
            });
        }
        [HttpGet("exam-list-{id}")]
        public async Task<IActionResult> GetExamByIdCategory(Guid id)
        {
            var query = new GetExamByCategoryQuery(id);
            var result = await Sender.Send(query);
            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "No exam found"
                });
            }
            return Ok(new
            {
                success = true,
                data = result,
                message = "Get exam by category successfully"
            });
        }
        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] PaginatedRequest request)
        {
            var query = new GetExamPaginatedQuery(request.PageIndex, request.PageSize);
            var result = await Sender.Send(query);
            return Ok(new
            {
                success = true,
                data = result,
                message = "Get paginated exams successfully"
            });
        }
        [HttpGet("do-exam/{id}")]
        public async Task<IActionResult> GetExamToTake(Guid id)
        {
            var query = new GetExamToTakeQuery(id);
            var result = await Sender.Send(query);
            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "No exam found"
                });
            }
            return Ok(new
            {
                success = true,
                data = result,
                message = "Get exam by id successfully"
            });
        }
        [HttpGet("search-by-keyword")]
        public async Task<IActionResult> GetExamByKeyWord([FromQuery] ExamByKeyWordRequest request)
        {
            var query = new GetExamByKeyWordQuery(request.KeyWord);
            var result = await Sender.Send(query);
            return Ok(new
            {
                success = true,
                data = result,
                message = "Get exam by keyword successfully"
            });
        }
    }
}
