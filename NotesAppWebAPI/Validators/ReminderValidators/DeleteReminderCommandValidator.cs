using FluentValidation;
using NotesAppWebAPI.Commands;

namespace NotesAppWebAPI.Validators.ReminderValidators
{
    public class DeleteReminderCommandValidator : AbstractValidator<DeleteReminderCommand>
    {
        public DeleteReminderCommandValidator() 
        {
            RuleFor(c => c.ReminderId)
                .GreaterThan(0).WithMessage("ID must be greater than zero");
        }
    }
}
