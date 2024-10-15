using FluentValidation;

namespace Application.Features.UserOperationClaims.Commands.Create;

public class CreateUserOperationClaimCommandValidator : AbstractValidator<CreateUserOperationClaimCommand>
{
    public CreateUserOperationClaimCommandValidator()
    {
        RuleFor(cuocc => cuocc.UserId).GreaterThan(0);
        RuleFor(cuocc => cuocc.OperationClaimId).GreaterThan(0);
    }
}