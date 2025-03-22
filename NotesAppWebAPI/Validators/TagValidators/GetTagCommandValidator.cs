using FluentValidation;
using NotesAppWebAPI.Commands;

namespace NotesAppWebAPI.Validators.TagValidators
{
    public class GetTagCommandValidator : AbstractValidator<GetTagCommand>
    {
        public GetTagCommandValidator()
        {
            RuleFor(c => c.TagId)
                .GreaterThan(0).WithMessage("ID must be greater than zero");
        }
    }
}
