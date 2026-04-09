using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FileServices
{
    public class FileService : IUploadImageService
    {
        public async Task<string> SaveImageAsync(Stream input, string fileExtension)
        {
                var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "images");
                if(!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var path = Path.Combine(rootPath, fileName);
                //var fileStreamOptions = new FileStreamOptions
                //{
                //    Mode = FileMode.Create,
                //    Access = FileAccess.Write,
                //    Options = FileOptions.Asynchronous,
                //    BufferSize = 8192
                //};
                using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    await input.CopyToAsync(fileStream);
                }
                return $"uploads/images/{fileName}";
        }
    }
}
