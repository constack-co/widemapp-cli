using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class TemplateConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.Property(x => x.Name)
                 .HasColumnName("Name")
                 .IsRequired();

            builder.Property(x => x.MicroserviceId)
                 .HasColumnName("MicroserviceId")
                 .IsRequired();

            Relations(builder);

            builder.ToTable("Templates");
        }

        private static void Relations(EntityTypeBuilder<Template> builder)
        {
            builder.HasOne(x => x.Microservice)
                .WithMany(x => x.Templates)
                .HasForeignKey(x => x.MicroserviceId);
        }
    }
}
