using Application.Abstractions.Messaging;
using Application.Models.File;

namespace Application.Features.File.Commands
{
    public sealed record UploadImageCommand(Stream Content, string FileName, string ContentType) : ICommand<UploadFileResponse>;
}
