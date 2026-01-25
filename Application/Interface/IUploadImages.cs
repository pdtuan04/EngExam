using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUploadImages
    {
        Task<string> UploadImageAsync(Stream stream, string fileExtension);
    }
}
