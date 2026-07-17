using Application.Common.Interfaces;
using Application.Models.File;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FileServices
{
    public class LocalStorageService : IFileService
    {
        private readonly LocalStorageOptions _localStorageOptions;
        public LocalStorageService(LocalStorageOptions localStorageOptions)
        {
            _localStorageOptions = localStorageOptions ?? throw new ArgumentNullException(nameof(localStorageOptions));
        }
        public async Task<UploadFileResponse> UploadImageAsync(Stream Content, string FileName, string ContentType)
        {
                var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _localStorageOptions.StoragePath);
                if(!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(FileName)}";
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
                    await Content.CopyToAsync(fileStream);
                }
                return new UploadFileResponse($"images/{fileName}", $"{_localStorageOptions.StoragePath}/{fileName}");
        }

        public async Task<UploadFileResponse> UploadAudioAsync(Stream Content, string FileName, string ContentType)
        {
            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _localStorageOptions.StoragePath);
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(FileName)}";
            var path = Path.Combine(rootPath, fileName);
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                await Content.CopyToAsync(fileStream);
            }
            return new UploadFileResponse($"audio/{fileName}", $"{_localStorageOptions.StoragePath}/{fileName}");
        }
    }

    public sealed class LocalStorageOptions
    {
        public string StoragePath { get; set; } = Path.Combine("images");
    }
}
