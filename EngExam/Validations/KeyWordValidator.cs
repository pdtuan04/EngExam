using Application.Models.Exam;
using FluentValidation;

namespace EngExam.Validations
{
    public sealed class KeyWordValidator : AbstractValidator<ExamByKeyWordRequest>
    {
        public KeyWordValidator() 
        {
            RuleFor(x => x.KeyWord)
                .NotEmpty().WithMessage("KeyWord is required.")
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("KeyWord must not be empty or whitespace.")
                .MinimumLength(5).WithMessage("KeyWord must be at least 5 characters.")
                .MaximumLength(50).WithMessage("KeyWord must not exceed 50 characters.");
        }
    }
}
