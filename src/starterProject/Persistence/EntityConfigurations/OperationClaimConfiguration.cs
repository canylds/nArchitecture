using System.Reflection;
using Application;
using Core.Security.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();

        builder.HasMany(oc => oc.UserOperationClaims).WithOne(uoc => uoc.OperationClaim).HasForeignKey(uoc => uoc.OperationClaimId);

        builder.HasIndex(indexExpression: o => o.Name, name: "UK_OperationClaims_Name").IsUnique();

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasData(_seeds);
    }

    private IEnumerable<OperationClaim> _seeds
    {
        get
        {
            int id = 0;

            yield return new OperationClaim(id: ++id, name: GeneralOperationClaims.Admin);

            #region Feature Operation Claims
            IEnumerable<Type> featureOperationClaimsTypes = Assembly
                .GetAssembly(typeof(ApplicationServiceRegistration))!
                .GetTypes()
                .Where(type =>
                (type.Namespace?.Contains("Features") == true)
                && (type.Namespace?.Contains("Constants") == true)
                && type.IsClass
                && type.Name.EndsWith("OperationClaims"));

            foreach (Type type in featureOperationClaimsTypes)
            {
                FieldInfo[] typeFields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
                IEnumerable<string> typeFieldsValues = typeFields.Select(@field => @field.GetValue(null)!.ToString()!);

                IEnumerable<OperationClaim> featureOperationClaimsToAdd =
                    typeFieldsValues.Select(value => new OperationClaim(id: ++id, name: value));

                foreach (OperationClaim featureOperationClaim in featureOperationClaimsToAdd)
                    yield return featureOperationClaim;
            }
            #endregion
        }
    }
}
