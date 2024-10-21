using Core.Security.Entities;

namespace Domain.Entities;

public class OtpAuthenticator : OtpAuthenticator<int, int>
{
    public virtual User User { get; set; } = default!;

    public OtpAuthenticator()
    {

    }

    public OtpAuthenticator(int userId, byte[] secretKey, bool isVerified) : base(userId, secretKey, isVerified)
    {

    }

    public OtpAuthenticator(int id, int userId, byte[] secretKey, bool isVerified) : base(id, userId, secretKey, isVerified)
    {

    }
}
