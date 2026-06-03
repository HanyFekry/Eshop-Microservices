using FluentValidation;

namespace Identity.Application.Commands
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
        }
    }
}
