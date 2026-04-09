using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.File
{
    public class UploadImageRequest
    {
        public required Stream Content { get; set; }
        public required string FileName { get; set; }
    }
}
