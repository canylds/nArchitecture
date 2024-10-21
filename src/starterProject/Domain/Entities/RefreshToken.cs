using Core.Security.Entities;

namespace Domain.Entities;

public class RefreshToken : RefreshToken<int, int>
{
    public virtual User User { get; set; } = default!;

    public RefreshToken()
    {

    }

    public RefreshToken(int userId, string token, DateTime expirationDate, string createdByIp)
        : base(userId, token, expirationDate, createdByIp)
    {

    }

    public RefreshToken(int id, int userId, string token, DateTime expirationDate, string createdByIp)
        : base(id, userId, token, expirationDate, createdByIp)
    {

    }
}
