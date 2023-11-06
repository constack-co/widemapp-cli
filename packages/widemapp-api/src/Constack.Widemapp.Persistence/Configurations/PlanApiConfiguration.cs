using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class PlanApiConfiguration : IEntityTypeConfiguration<PlanApi>
    {
        public void Configure(EntityTypeBuilder<PlanApi> builder)
        {
            builder.Property(x => x.PlanId)
                 .HasColumnName("PlanId")
                 .IsRequired(false);

            builder.Property(x => x.ApiId)
                 .HasColumnName("ApiId")
                 .IsRequired(false);

            builder.Property(x => x.WebComponentId)
                 .HasColumnName("WebComponentId")
                 .IsRequired(false);

            Relations(builder);

            builder.ToTable("PlanApis");
        }

        private static void Relations(EntityTypeBuilder<PlanApi> builder)
        {
            builder.HasOne(x => x.Plan)
                .WithMany(x => x.PlanApis)
                .HasForeignKey(x => x.PlanId);    
            
            builder.HasOne(x => x.Api)
                .WithMany(x => x.PlanApis)
                .HasForeignKey(x => x.ApiId);
            
            builder.HasOne(x => x.WebComponent)
                .WithMany(x => x.PlanApis)
                .HasForeignKey(x => x.WebComponentId);
        }
    }
}
