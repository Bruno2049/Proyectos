using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EntityPrueba.Repositorio.Models.Mapping
{
    public class AspNetUserRoleMap : EntityTypeConfiguration<IdentityUserRole>
    {
        public AspNetUserRoleMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UserId, t.RoleId });

            // Table & Column Mappings
            this.ToTable("AspNetUserRoles");
            this.Property(t => t.UserId)
				.IsRequired()
				.HasMaxLength(128)
				.HasColumnName("UserId");
            this.Property(t => t.RoleId)
				.IsRequired()
				.HasMaxLength(128)
				.HasColumnName("RoleId");

         
			
        }
    }
}
