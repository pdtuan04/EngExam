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
    public sealed class UploadAudioCommandHandler : ICommandHandler<UploadAudioCommand, UploadFileResponse>
    {
        private readonly IFileService _fileService;
        public UploadAudioCommandHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<UploadFileResponse> Handle(UploadAudioCommand request, CancellationToken cancellationToken)
        {
            if (request.Content.CanSeek) request.Content.Seek(0, SeekOrigin.Begin);
            var savingAudio = await _fileService.UploadAudioAsync(request.Content, request.FileName, request.ContentType);
            return savingAudio;
        }
    }
}
