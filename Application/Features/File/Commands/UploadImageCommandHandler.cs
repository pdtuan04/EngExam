using Application.Abstractions.Messaging;
using Application.Common.Interfaces;
using Application.Models.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.File.Commands
{
    public sealed class UploadImageCommandHandler : ICommandHandler<UploadImageCommand, UploadFileResponse>
    {
        private readonly IFileService _fileService;
        public UploadImageCommandHandler(IFileService fileService)
        {
            _fileService = fileService;
        }
        public async Task<UploadFileResponse> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            if (request.Content.CanSeek) request.Content.Seek(0, SeekOrigin.Begin);
            var savingImage = await _fileService.UploadImageAsync(request.Content,request.FileName, request.ContentType);
            return savingImage;
        }
    }
}
