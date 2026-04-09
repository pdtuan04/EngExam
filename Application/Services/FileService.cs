using Application.Common.Interfaces;
using Application.Models.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FileService : IFileService
    {
        private readonly IUploadImageService _uploadImageService;
        public FileService(IUploadImageService uploadImageService)
        {
            _uploadImageService = uploadImageService;
        }
        public Task<string> UploadImageAsync(UploadImageRequest request)
        {
            if (request.Content.CanSeek) request.Content.Seek(0, SeekOrigin.Begin);
            var savingImage = _uploadImageService.SaveImageAsync(request.Content, request.FileName);
            return savingImage;
        }
    }
}
