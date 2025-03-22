using FluentValidation;
using NotesAppWebAPI.Commands;

namespace NotesAppWebAPI.Validators.TagValidators
{
    public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
    {
        public DeleteTagCommandValidator()
        {
            RuleFor(c => c.TagId)
                .GreaterThan(0).WithMessage("ID must be greater than zero");
        }
    }
}
