using FluentValidation;
using NotesAppWebAPI.Commands;

namespace NotesAppWebAPI.Validators.NoteValidators
{
    public class GetNoteCommandValidator : AbstractValidator<GetNoteCommand>
    {
        public GetNoteCommandValidator()
        {
            RuleFor(c => c.NoteId)
                .GreaterThan(0).WithMessage("ID must be greater than zero");
        }
    }
}
