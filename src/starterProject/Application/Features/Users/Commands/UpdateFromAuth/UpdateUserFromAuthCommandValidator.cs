using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.Users.Commands.UpdateFromAuth;

public class UpdateUserFromAuthCommandValidator : AbstractValidator<UpdateUserFromAuthCommand>
{
    public UpdateUserFromAuthCommandValidator()
    {
        RuleFor(uufac => uufac.FirstName).NotEmpty().MinimumLength(2);
        RuleFor(uufac => uufac.LastName).NotEmpty().MinimumLength(2);
        RuleFor(uufac => uufac.Password).NotEmpty().MinimumLength(8);
        RuleFor(uufac => uufac.NewPassword)
        .NotEmpty()
        .MinimumLength(8)
        .Must(StrongPassword)
        .WithMessage("Your password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, and one special character.");
    }

    private bool StrongPassword(string arg)
    {
        Regex regex = new("/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/");

        return regex.IsMatch(arg);
    }
}