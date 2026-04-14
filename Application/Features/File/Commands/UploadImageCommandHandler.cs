using Application.Abstractions.Messaging;
using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.File.Commands
{
    public sealed class UploadImageCommandHandler : ICommandHandler<UploadImageCommand, string>
    {
        private readonly IUploadImageService _uploadImageService;
        public UploadImageCommandHandler(IUploadImageService uploadImageService)
        {
            _uploadImageService = uploadImageService;
        }
        public async Task<string> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            if (request.Content.CanSeek) request.Content.Seek(0, SeekOrigin.Begin);
            var savingImage = await _uploadImageService.SaveImageAsync(request.Content, request.FileName);
            return savingImage;
        }
    }
}
