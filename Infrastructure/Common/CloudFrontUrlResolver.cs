using Application.Common.Interfaces;
using Infrastructure.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public sealed class CloudFrontUrlResolver(S3Options s3Options) : IFileUrlResolver
    {
        public string ResolveFileUrl(string fileName)   
        {
            return $"{s3Options.CloudFrontDomain}/{fileName}";
        }
    }
}
