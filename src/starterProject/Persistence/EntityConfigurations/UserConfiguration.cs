using Core.Security.Enums;
using Core.Security.Hashing;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(u => u.Id);

        builder.Property(u => u.Id).HasColumnName("Id").IsRequired();
        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");
        builder.Property(u => u.Email).HasColumnName("Email").IsRequired();
        builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash").IsRequired();
        builder.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt").IsRequired();
        builder.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType").IsRequired();
        builder.Property(u => u.FirstName).HasColumnName("FirstName").IsRequired();
        builder.Property(u => u.LastName).HasColumnName("LastName").IsRequired();

        builder.HasMany(u => u.UserOperationClaims).WithOne(uoc => uoc.User).HasForeignKey(uoc => uoc.UserId);
        builder.HasMany(u => u.RefreshTokens).WithOne(rt => rt.User).HasForeignKey(rt => rt.UserId);
        builder.HasMany(u => u.EmailAuthenticators).WithOne(ea => ea.User).HasForeignKey(ea => ea.UserId);
        builder.HasMany(u => u.OtpAuthenticators).WithOne(oa => oa.User).HasForeignKey(oa => oa.UserId);

        builder.HasIndex(indexExpression: u => u.Email, name: "UK_Users_Email").IsUnique();

        builder.HasQueryFilter(u => !u.DeletedDate.HasValue);

        builder.HasData(_seeds);
    }

    private IEnumerable<User> _seeds
    {
        get
        {
            int id = 0;

            HashingHelper.CreatePasswordHash(password: "Passw0rd!",
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt);

            User adminUser = new(id: ++id,
                email: "narch@example.com",
                passwordHash: passwordHash,
                passwordSalt: passwordSalt,
                authenticatorType: AuthenticatorType.None,
                firstName: "John",
                lastName: "Doe");

            yield return adminUser;
        }
    }
}
