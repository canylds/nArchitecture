using FluentValidation;

namespace Application.Features.Users.Commands.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(uuc => uuc.Email).NotEmpty().EmailAddress();
        RuleFor(uuc => uuc.Password).NotEmpty().MinimumLength(4);
        RuleFor(uuc => uuc.FirstName).NotEmpty().MinimumLength(2);
        RuleFor(uuc => uuc.LastName).NotEmpty().MinimumLength(2);
    }
}