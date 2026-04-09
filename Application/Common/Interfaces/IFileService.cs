using Application.Models.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(UploadImageRequest request);
    }
}
