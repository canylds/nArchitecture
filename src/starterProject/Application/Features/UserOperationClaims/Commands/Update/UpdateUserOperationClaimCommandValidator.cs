using FluentValidation;

namespace Application.Features.UserOperationClaims.Commands.Update;

public class UpdateUserOperationClaimCommandValidator : AbstractValidator<UpdateUserOperationClaimCommand>
{
    public UpdateUserOperationClaimCommandValidator()
    {
        RuleFor(uuocc => uuocc.UserId).GreaterThan(0);
        RuleFor(uuocc => uuocc.OperationClaimId).GreaterThan(0);
    }
}