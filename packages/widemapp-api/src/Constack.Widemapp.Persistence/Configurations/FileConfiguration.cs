using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<Domain.Entities.File>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.File> builder)
        {
            builder.Property(x => x.Name)
                 .HasColumnName("Name")
                 .IsRequired();

            builder.Property(x => x.Path)
                 .HasColumnName("Path")
                 .IsRequired();

            builder.Property(x => x.ContentAdd)
                 .HasColumnName("ContentAdd")
                 .IsRequired(false);

            builder.Property(x => x.Action)
                 .HasColumnName("Action")
                 .IsRequired();

            builder.Property(x => x.Placeholder)
                 .HasColumnName("Placeholder")
                 .IsRequired();

            builder.Property(x => x.TemplateId)
                 .HasColumnName("TemplateId")
                 .IsRequired();

            builder.Property(x => x.GroupId)
                 .HasColumnName("GroupId")
                 .IsRequired();

            Relations(builder);

            builder.ToTable("Files");
        }

        private static void Relations(EntityTypeBuilder<Domain.Entities.File> builder)
        {
            builder.HasOne(x => x.Template)
                .WithMany(x => x.Files)
                .HasForeignKey(x => x.TemplateId);

            builder.HasOne(x => x.Group)
                .WithMany(x => x.Files)
                .HasForeignKey(x => x.GroupId);
        }
    }
}
