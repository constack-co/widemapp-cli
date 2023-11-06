using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class EntityPropertyConfiguration : IEntityTypeConfiguration<EntityProperty>
    {
        public void Configure(EntityTypeBuilder<EntityProperty> builder)
        {
            builder.Property(x => x.EntityId)
                .HasColumnName("EntityId")
                .IsRequired();

            builder.Property(x => x.PropertyId)
                 .HasColumnName("PropertyId")
                 .IsRequired();

            Relations(builder);

            builder.ToTable("EntityProperties");
        }

        private static void Relations(EntityTypeBuilder<EntityProperty> builder)
        {
            builder.HasOne(x => x.Entity)
                .WithMany(x => x.EntityProperties)
                .HasForeignKey(x => x.EntityId);

            builder.HasOne(x => x.Property)
                .WithMany(x => x.EntityProperties)
                .HasForeignKey(x => x.PropertyId);
        }
    }
}
