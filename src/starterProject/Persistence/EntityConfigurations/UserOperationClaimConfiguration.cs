using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
{
    public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
    {
        builder.ToTable("UserOperationClaims").HasKey(uoc => uoc.Id);

        builder.Property(uoc => uoc.Id).HasColumnName("Id").IsRequired();
        builder.Property(uoc => uoc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(uoc => uoc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(uoc => uoc.DeletedDate).HasColumnName("DeletedDate");
        builder.Property(uoc => uoc.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(uoc => uoc.OperationClaimId).HasColumnName("OperationClaimId").IsRequired();

        builder.HasOne(uoc => uoc.User).WithMany(u => u.UserOperationClaims).HasForeignKey(uoc => uoc.UserId);
        builder.HasOne(uoc => uoc.OperationClaim).WithMany(o => o.UserOperationClaims).HasForeignKey(uoc => uoc.OperationClaimId);

        builder.HasIndex(indexExpression: uoc => new
        {
            uoc.UserId,
            uoc.OperationClaimId
        }, name: "UK_UserOperationClaims_UserId_OperationClaimId").IsUnique();

        builder.HasQueryFilter(uoc => !uoc.DeletedDate.HasValue);

        builder.HasData(_seeds);
    }

    private IEnumerable<UserOperationClaim> _seeds
    {
        get
        {
            int id = 0;

            yield return new UserOperationClaim(id: ++id, userId: 1, operationClaimmId: 1);
        }
    }
}
