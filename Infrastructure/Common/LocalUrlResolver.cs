using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public sealed class LocalUrlResolver : IFileUrlResolver
    {
        public string ResolveFileUrl(string fileName)
        {
            return fileName;
        }
    }
}
