using Core.Security.Entities;

namespace Domain.Entities;

public class EmailAuthenticator : EmailAuthenticator<int, int>
{
    public virtual User User { get; set; } = default!;

    public EmailAuthenticator()
    {

    }

    public EmailAuthenticator(int userId, bool isVerified) : base(userId, isVerified)
    {

    }

    public EmailAuthenticator(int id, int userId, bool isVerified) : base(id, userId, isVerified)
    {

    }
}
