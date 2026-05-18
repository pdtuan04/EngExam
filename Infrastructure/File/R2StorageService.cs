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
        public Task<string> UploadImageAsync(Stream Content, string FileName, string ContentType)
        {
            //not implemented yet, we will implement it when we need to use it
            throw new NotImplementedException();
        }
    }
}
