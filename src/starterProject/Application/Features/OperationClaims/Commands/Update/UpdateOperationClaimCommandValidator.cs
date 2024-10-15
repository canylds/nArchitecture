using FluentValidation;

namespace Application.Features.OperationClaims.Commands.Update;

public class UpdateOperationClaimCommandValidator : AbstractValidator<UpdateOperationClaimCommand>
{
    public UpdateOperationClaimCommandValidator()
    {
        RuleFor(uocc => uocc.Name).NotEmpty().MinimumLength(2);
    }
}