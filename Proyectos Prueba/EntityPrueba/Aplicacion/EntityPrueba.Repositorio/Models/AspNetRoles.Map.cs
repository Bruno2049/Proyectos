using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EntityPrueba.Repositorio.Models.Mapping
{
    public class AspNetRoleMap : EntityTypeConfiguration<IdentityRole>
    {
        public AspNetRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .IsRequired()
				.HasMaxLength(128)
				.HasColumnName("Id");

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256)
				.HasColumnName("Name")
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex") {IsClustered = false, IsUnique = true}));

            // Table & Column Mappings
            this.ToTable("AspNetRoles");
			
			this.HasMany(t => t.Users)
                .WithRequired()
                .HasForeignKey(ur => ur.RoleId);
					
        }
    }
}
