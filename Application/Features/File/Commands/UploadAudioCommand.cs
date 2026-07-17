using Application.Abstractions.Messaging;
using Application.Models.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.File.Commands
{
    public sealed record UploadAudioCommand(Stream Content, string FileName, string ContentType) : ICommand<UploadFileResponse>;
}
