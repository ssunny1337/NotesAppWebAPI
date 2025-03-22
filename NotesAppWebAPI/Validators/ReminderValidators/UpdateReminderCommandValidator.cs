using FluentValidation;
using NotesAppWebAPI.Commands;

namespace NotesAppWebAPI.Validators.ReminderValidators
{
    public class UpdateReminderCommandValidator : AbstractValidator<UpdateReminderCommand>
    {
        public UpdateReminderCommandValidator()
        {
            RuleFor(c => c.ReminderId)
                .GreaterThan(0).WithMessage("ID must be greater than zero");

            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title must be less than 100 characters long");

            RuleFor(c => c.Text)
                .NotEmpty().WithMessage("Text is required");

            RuleFor(c => c.ReminderTime)
                .GreaterThan(DateTime.UtcNow).WithMessage("Reminder date must be in the future.");

            RuleFor(c => c.TagsIds)
                .Must(tags => tags == null || tags.All(id => id > 0))
                .WithMessage("All tag IDs must be greater than zero");
        }
    }
}
