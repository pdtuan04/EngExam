using Application.Interface.Media;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadMediaController : ControllerBase
    {
        private readonly IUploadImages _uploadImages;
        public UploadMediaController(IUploadImages uploadImages)
        {
            _uploadImages = uploadImages;
        }
        [HttpPost("upload-images")]
        public async Task<IActionResult> UploadImages(IFormFile file)
        {
            var path = await _uploadImages.UploadImageAsync(file.OpenReadStream(), Path.GetExtension(file.FileName));

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
