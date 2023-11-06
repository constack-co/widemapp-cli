using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasColumnName("LastName")
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
               .HasColumnName("PhoneNumber")
               .IsRequired();

            builder.Property(x => x.UserName)
                .HasColumnName("UserName")
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .IsRequired(false);

            Relations(builder);

            builder.ToTable("Users");
        }

        private static void Relations(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(e => e.UserRoles)
              .WithOne()
              .HasForeignKey(ur => ur.UserId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
