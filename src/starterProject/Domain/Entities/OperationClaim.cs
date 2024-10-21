using Core.Security.Entities;

namespace Domain.Entities;

public class OperationClaim : OperationClaim<int>
{
    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = default!;

    public OperationClaim()
    {

    }

    public OperationClaim(string name) : base(name)
    {

    }

    public OperationClaim(int id, string name) : base(id, name)
    {

    }
}
