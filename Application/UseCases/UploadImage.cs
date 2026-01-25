using Application.Handler.InterfaceHandler;
using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class UploadImage : IUploadImages
    {
        private readonly ISaveImageHandler _saveImageHandler;
        private const long MaxImageSizeInBytes = 2 * 1024 * 1024;
        private static readonly HashSet<string> AllowedImageExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg",
            ".jpeg",
            ".png",
        };
        public UploadImage(ISaveImageHandler saveImageHandler)
        {
            _saveImageHandler = saveImageHandler;
        }
        public Task<string> UploadImageAsync(Stream stream, string fileExtension)
        {
            if(stream.Length > MaxImageSizeInBytes)
                throw new Exception("File is too large");

            if(!AllowedImageExtensions.Contains(fileExtension))
                throw new Exception("File type is not allowed");
            //check if we can use the poniter and move it to the begining
            if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
            var savingImage = _saveImageHandler.SaveImageAsync(stream, fileExtension);
            return savingImage;
        }
    }
}
