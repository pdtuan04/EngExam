using Application.Models.File;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validations
{
    public class UploadImageValidator : AbstractValidator<UploadImageRequest>
    {
        private const long MaxFileSizeInBytes = 5 * 1024 * 1024;
        public UploadImageValidator()
        {
            RuleFor(f => f.Content)
                .NotNull().WithMessage("File is required.");
            RuleFor(f => f.Content.Length)
                .GreaterThan(0).WithMessage("File is empty.")
                .LessThanOrEqualTo(MaxFileSizeInBytes).WithMessage($"File size is only 5 MB.");
            RuleFor(f => Path.GetExtension(f.FileName).ToLowerInvariant())
                .Must(ext => new[] { ".jpg", ".png", ".jpeg" }.Contains(ext))
                .WithMessage("Only .jpg, .jpeg, .png files are allowed.");
        }
    }
}
