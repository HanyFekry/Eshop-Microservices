using FluentValidation;

namespace Identity.Application.Commands
{
    public class RotateRefreshTokenCommandValidator : AbstractValidator<RotateRefreshTokenCommand>
    {
        public RotateRefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
