using Application.Common.Interfaces;
using Application.Models.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadMediaController : ControllerBase
    {
        private readonly IFileService _uploadImages;
        public UploadMediaController(IFileService uploadImages)
        {
            _uploadImages = uploadImages;
        }
        [HttpPost("upload-images")]
        public async Task<IActionResult> UploadImages(IFormFile file)
        {
            var request = new UploadImageRequest { 
                Content = file.OpenReadStream(),
                FileName = file.FileName
            };
            var path = await _uploadImages.UploadImageAsync(request);

            return Ok(new
            {
                success = true,
                data = path,
                message = "Upload image successfully"
            });
        }
        [HttpPost("upload-videos")]
        public async Task<IActionResult> UploadVideos(IFormFile file)
        {
            return Ok("Media uploaded successfully.");
        }
    }
}
