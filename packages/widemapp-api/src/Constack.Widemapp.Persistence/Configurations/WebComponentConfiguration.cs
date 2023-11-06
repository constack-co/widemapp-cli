using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class WebComponentConfiguration : IEntityTypeConfiguration<WebComponent>
    {
        public void Configure(EntityTypeBuilder<WebComponent> builder)
        {
            builder.Property(x => x.Name)
                 .HasColumnName("Name")
                 .IsRequired();

            builder.Property(x => x.Menu)
                 .HasColumnName("Menu")
                 .IsRequired(false);

            builder.Property(x => x.Path)
                 .HasColumnName("Path")
                 .IsRequired(false);

            builder.Property(x => x.TemplateId)
                 .HasColumnName("TemplateId")
                 .IsRequired(false);

            Relations(builder);

            builder.ToTable("WebComponents");
        }

        private static void Relations(EntityTypeBuilder<WebComponent> builder)
        {
            builder.HasOne(x => x.Template)
                .WithMany(x => x.WebComponents)
                .HasForeignKey(x => x.TemplateId);
        }
    }
}
