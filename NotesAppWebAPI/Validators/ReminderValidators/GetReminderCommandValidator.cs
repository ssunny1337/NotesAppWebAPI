using FluentValidation;
using NotesAppWebAPI.Commands;

namespace NotesAppWebAPI.Validators.ReminderValidators
{
    public class GetReminderCommandValidator : AbstractValidator<GetReminderCommand>
    {
        public GetReminderCommandValidator()
        {
            RuleFor(c => c.ReminderId)
                .GreaterThan(0).WithMessage("ID must be greater than zero");
        }
    }
}
