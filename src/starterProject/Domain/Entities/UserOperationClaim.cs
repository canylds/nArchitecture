using Core.Security.Entities;

namespace Domain.Entities;

public class UserOperationClaim : UserOperationClaim<int, int, int>
{
    public virtual User User { get; set; } = default!;
    public virtual OperationClaim OperationClaim { get; set; } = default!;

    public UserOperationClaim()
    {

    }

    public UserOperationClaim(int userId, int operationClaimId) : base(userId, operationClaimId)
    {

    }

    public UserOperationClaim(int id, int userId, int operationClaimmId) : base(id, userId, operationClaimmId)
    {

    }
}
