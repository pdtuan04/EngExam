using Amazon.S3;
using Amazon.S3.Model;
using Application.Common.Interfaces;
using Application.Models.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;
using System.Net;
namespace Infrastructure.File
{
    public class S3StorageService : IFileService
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly S3Options _s3Options;
        public S3StorageService(IAmazonS3 amazonS3, S3Options s3Options)
        {
            _amazonS3 = amazonS3 ?? throw new ArgumentNullException(nameof(amazonS3));
            _s3Options = s3Options ?? throw new ArgumentNullException(nameof(s3Options));
        }
        public async Task<string> UploadImageAsync(Stream Content, string FileName, string ContentType)
        {
            var key = $"images/{Guid.NewGuid()}_{FileName}";
            var putObjectRequest = new PutObjectRequest
            {
                BucketName = _s3Options.BucketName,
                Key = key,
                InputStream = Content,
                ContentType = ContentType,
                Metadata =
                {
                    ["file-name"] = FileName
                }
            };
            var response = await _amazonS3.PutObjectAsync(putObjectRequest);
            if(response.HttpStatusCode == HttpStatusCode.OK)
            {
                return key;
            }
            throw new Exception("Failed to upload image to S3");
        }
    }
    public sealed class S3Options
    {
        public string BucketName { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string CloudFrontDomain { get; set; } = null!;
    }
}
