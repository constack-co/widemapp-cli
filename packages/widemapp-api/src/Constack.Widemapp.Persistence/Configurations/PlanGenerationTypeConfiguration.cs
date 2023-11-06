using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class PlanGenerationTypeConfiguration : IEntityTypeConfiguration<PlanGenerationType>
    {
        public void Configure(EntityTypeBuilder<PlanGenerationType> builder)
        {
            builder.Property(x => x.PlanId)
                 .HasColumnName("PlanId")
                 .IsRequired();

            builder.Property(x => x.GenerationTypeId)
                 .HasColumnName("GenerationTypeId")
                 .IsRequired();

            Relations(builder);

            builder.ToTable("PlanGenerationTypes");
        }

        private static void Relations(EntityTypeBuilder<PlanGenerationType> builder)
        {
            builder.HasOne(x => x.Plan)
                .WithMany(x => x.PlanGenerationTypes)
                .HasForeignKey(x => x.PlanId);

            builder.HasOne(x => x.GenerationType)
                .WithMany(x => x.PlanGenerationTypes)
                .HasForeignKey(x => x.GenerationTypeId);
        }
    }
}
