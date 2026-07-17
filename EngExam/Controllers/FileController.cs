using Application.Common.Interfaces;
using Application.Features.File.Commands;
using Application.Models.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ApiController
    {
        [Authorize]
        [HttpPost("upload-images")]
        public async Task<IActionResult> UploadImages(IFormFile file)
        {
            var request = new UploadImageRequest(
                file.OpenReadStream(),
                file.FileName,
                file.ContentType
            );
            var command = new UploadImageCommand(request.Content, request.FileName, request.ContentType);
            var result = await Sender.Send(command);

            return Ok(new
            {
                success = true,
                data = result,
                message = "Upload image successfully"
            });
        }
        [Authorize]
        [HttpPost("upload-audio")]
        public async Task<IActionResult> UploadAudio(IFormFile file)
        {
            var request = new UploadAudioRequest(
                file.OpenReadStream(),
                file.FileName,
                file.ContentType
            );
            var command = new UploadAudioCommand(request.Content, request.FileName, request.ContentType);
            var result = await Sender.Send(command);
            return Ok(new
            {
                success = true,
                data = result,
                message = "Upload audio successfully"
            });
        }
        [HttpPost("upload-videos")]
        public async Task<IActionResult> UploadVideos(IFormFile file)
        {
            //not implemented yet, just return success for testing
            return Ok("Media uploaded successfully.");
        }
    }
}
