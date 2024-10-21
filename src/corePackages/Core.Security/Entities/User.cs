using Core.Persistence.Repositories;
using Core.Security.Enums;

namespace Core.Security.Entities;

public class User<TId> : Entity<TId>
{
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public AuthenticatorType AuthenticatorType { get; set; }

    public User()
    {
        Email = string.Empty;
        PasswordHash = [];
        PasswordSalt = [];
    }

    public User(string email, byte[] passwordHash, byte[] passwordSalt, AuthenticatorType authenticatorType)
    {
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        AuthenticatorType = authenticatorType;
    }

    public User(TId id, string email, byte[] passwordHash, byte[] passwordSalt, AuthenticatorType authenticatorType) : base(id)
    {
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        AuthenticatorType = authenticatorType;
    }
}
