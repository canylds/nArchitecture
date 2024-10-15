using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OtpAuthenticatorConfiguration : IEntityTypeConfiguration<OtpAuthenticator>
{
    public void Configure(EntityTypeBuilder<OtpAuthenticator> builder)
    {
        builder.ToTable("OtpAuthenticators").HasKey(oa => oa.Id);

        builder.Property(oa => oa.Id).HasColumnName("Id").IsRequired();
        builder.Property(oa => oa.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oa => oa.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oa => oa.DeletedDate).HasColumnName("DeletedDate");
        builder.Property(oa => oa.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(oa => oa.SecretKey).HasColumnName("SecretKey").IsRequired();
        builder.Property(oa => oa.IsVerified).HasColumnName("IsVerified").IsRequired();

        builder.HasOne(oa => oa.User).WithMany(u => u.OtpAuthenticators).HasForeignKey(oa => oa.UserId);

        builder.HasQueryFilter(oa => !oa.DeletedDate.HasValue);
    }
}
