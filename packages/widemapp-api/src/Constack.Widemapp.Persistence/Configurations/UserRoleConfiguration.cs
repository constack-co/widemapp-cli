using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Constack.Widemapp.Domain.Entities;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.Property(x => x.UserId)
               .HasColumnName("UserId")
               .IsRequired();

            builder.Property(x => x.RoleId)
               .HasColumnName("RoleId")
               .IsRequired();

            Relations(builder);

            builder.ToTable("UserRoles");
        }

        private static void Relations(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasOne(e => e.User)
              .WithMany(x => x.UserRoles)
              .HasForeignKey(ur => ur.UserId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Role)
              .WithMany(x => x.UserRoles)
              .HasForeignKey(ur => ur.RoleId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
