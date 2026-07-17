using Application.Common.Interfaces;
using Application.Models.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.File
{
    public sealed class R2StorageService : IFileService
    {
        public Task<UploadFileResponse> UploadImageAsync(Stream Content, string FileName, string ContentType)
        {
            //not implemented yet, i will implement it when i need to use it
            throw new NotImplementedException();
        }

        public Task<UploadFileResponse> UploadAudioAsync(Stream Content, string FileName, string ContentType)
        {
            //not implemented yet, i will implement it when i need to use it
            throw new NotImplementedException();
        }
    }
}
