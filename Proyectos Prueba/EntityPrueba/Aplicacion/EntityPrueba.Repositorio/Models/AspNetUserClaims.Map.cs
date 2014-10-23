using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EntityPrueba.Repositorio.Models.Mapping
{
    public class AspNetUserClaimMap : EntityTypeConfiguration<IdentityUserClaim>
    {
        public AspNetUserClaimMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("AspNetUserClaims");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ClaimType).HasColumnName("ClaimType");
            this.Property(t => t.ClaimValue).HasColumnName("ClaimValue");
			this.Property(t => t.UserId)
				.IsRequired()
				.HasMaxLength(128)
				.HasColumnName("UserId");
        }
    }
}
