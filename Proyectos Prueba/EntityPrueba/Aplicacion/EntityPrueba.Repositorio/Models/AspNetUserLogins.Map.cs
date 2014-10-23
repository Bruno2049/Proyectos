using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EntityPrueba.Repositorio.Models.Mapping
{
    public class AspNetUserLoginMap : EntityTypeConfiguration<IdentityUserLogin>
    {
        public AspNetUserLoginMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });

            // Properties
            this.Property(t => t.UserId)
                .IsRequired()
				.HasMaxLength(128)
                
				.HasColumnName("UserId");

            this.Property(t => t.LoginProvider)
                .IsRequired()
                .HasMaxLength(128)
				.HasColumnName("LoginProvider");

            this.Property(t => t.ProviderKey)
                .IsRequired()
                .HasMaxLength(128)
				.HasColumnName("ProviderKey");

            // Table & Column Mappings
            this.ToTable("AspNetUserLogins");

            
        }
    }
}
