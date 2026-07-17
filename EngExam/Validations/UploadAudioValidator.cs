using Application.Models.File;
using FluentValidation;

namespace EngExam.Validations
{
    public sealed class UploadAudioValidator : AbstractValidator<UploadAudioRequest>
    {
        private const long MaxFileSizeInBytes = 10 * 1024 * 1024;
        public UploadAudioValidator() 
        { 
            RuleFor(f => f.Content)
                .NotNull().WithMessage("File is required.");
            RuleFor(f => f.Content.Length)
                .GreaterThan(0).WithMessage("File is empty.")
                .LessThanOrEqualTo(MaxFileSizeInBytes).WithMessage($"File size is only 10 MB.");
            RuleFor(f => Path.GetExtension(f.FileName).ToLowerInvariant())
                .Must(ext => new[] { ".mp3", ".wav", ".aac" }.Contains(ext))
                .WithMessage("Only .mp3, .wav, .aac files are allowed.");
        }
    }
}
