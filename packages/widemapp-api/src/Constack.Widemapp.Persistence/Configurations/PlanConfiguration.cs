using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(x => x.Name)
                 .HasColumnName("Name")
                 .IsRequired();

            builder.Property(x => x.MicroserviceId)
                 .HasColumnName("MicroserviceId")
                 .IsRequired(false);
            
            builder.Property(x => x.MainEntityId)
                 .HasColumnName("MainEntityId")
                 .IsRequired();
            
            builder.Property(x => x.UserId)
                 .HasColumnName("UserId")
                 .IsRequired();

            builder.Property(x => x.TemplateId)
                 .HasColumnName("TemplateId")
                 .IsRequired();

            Relations(builder);

            builder.ToTable("Plans");
        }

        private static void Relations(EntityTypeBuilder<Plan> builder)
        {
            builder.HasOne(x => x.Microservice)
                .WithMany(x => x.Plans)
                .HasForeignKey(x => x.MicroserviceId);
            
            builder.HasOne(x => x.Entity)
                .WithMany(x => x.Plans)
                .HasForeignKey(x => x.MainEntityId);
            
            builder.HasOne(x => x.Template)
                .WithMany(x => x.Plans)
                .HasForeignKey(x => x.TemplateId);
        }
    }
}
