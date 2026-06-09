using FluentValidation;

namespace Identity.Application.Commands
{
    public class SetUserActiveCommandValidator : AbstractValidator<SetUserActiveCommand>
    {
        public SetUserActiveCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
        }
    }
}
