using FluentValidation;

namespace Application.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimCommandValidator : AbstractValidator<CreateOperationClaimCommand>
{
    public CreateOperationClaimCommandValidator()
    {
        RuleFor(cocc => cocc.Name).NotEmpty().MinimumLength(2);
    }
}