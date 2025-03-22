using FluentValidation;
using NotesAppWebAPI.Commands;

namespace NotesAppWebAPI.Validators.NoteValidators
{
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator()
        {
            RuleFor(c => c.NoteId)
                .GreaterThan(0).WithMessage("ID must be greater than zero");
        }
    }
}
