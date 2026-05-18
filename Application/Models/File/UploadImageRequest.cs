using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.File
{
    public sealed record UploadImageRequest(
    Stream Content,
    string FileName,
    string ContentType);
}
