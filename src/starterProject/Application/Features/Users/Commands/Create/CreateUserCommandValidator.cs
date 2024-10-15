using FluentValidation;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(cuc => cuc.FirstName).NotEmpty().MinimumLength(2);
        RuleFor(cuc => cuc.LastName).NotEmpty().MinimumLength(2);
        RuleFor(cuc => cuc.Email).NotEmpty().EmailAddress();
        RuleFor(cuc => cuc.Password).NotEmpty().MinimumLength(4);
    }
}