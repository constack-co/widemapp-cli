using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(x => x.Name)
                 .HasColumnName("Name")
                 .IsRequired();

            builder.Property(x => x.UserId)
                 .HasColumnName("UserId")
                 .IsRequired();

            Relations(builder);

            builder.ToTable("Projects");
        }

        private static void Relations(EntityTypeBuilder<Project> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.UserId);
        }
    }
}
