using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class PlanGroupConfiguration : IEntityTypeConfiguration<PlanGroup>
    {
        public void Configure(EntityTypeBuilder<PlanGroup> builder)
        {
            builder.Property(x => x.PlanId)
                 .HasColumnName("PlanId")
                 .IsRequired();

            builder.Property(x => x.GroupName)
                 .HasColumnName("GroupName")
                 .IsRequired();

            Relations(builder);

            builder.ToTable("PlanGroups");
        }

        private static void Relations(EntityTypeBuilder<PlanGroup> builder)
        {
            builder.HasOne(x => x.Plan)
                .WithMany(x => x.PlanGroups)
                .HasForeignKey(x => x.PlanId);
        }
    }
}
