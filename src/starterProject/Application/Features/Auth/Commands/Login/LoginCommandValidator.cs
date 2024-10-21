using FluentValidation;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(lc => lc.UserForLoginDto.Email).NotEmpty().EmailAddress();
        RuleFor(lc => lc.UserForLoginDto.Password).NotEmpty().MinimumLength(4);
    }
}
