using Core.Security.Entities;
using Core.Security.Enums;

namespace Domain.Entities;

public class User : User<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = default!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
    public virtual ICollection<OtpAuthenticator> OtpAuthenticators { get; set; } = default!;
    public virtual ICollection<EmailAuthenticator> EmailAuthenticators { get; set; } = default!;

    public User()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public User(string email,
        byte[] passwordSalt,
        byte[] passwordHash,
        AuthenticatorType authenticatorType,
        string firstname,
        string lastname) : base(email, passwordSalt, passwordHash, authenticatorType)
    {
        FirstName = firstname;
        LastName = lastname;
    }

    public User(int id,
        string email,
        byte[] passwordSalt,
        byte[] passwordHash,
        AuthenticatorType authenticatorType,
        string firstname,
        string lastname) : base(id, email, passwordSalt, passwordHash, authenticatorType)
    {
        FirstName = firstname;
        LastName = lastname;
    }
}
