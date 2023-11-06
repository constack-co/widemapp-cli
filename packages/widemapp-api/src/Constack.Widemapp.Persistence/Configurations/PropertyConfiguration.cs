using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.Property(x => x.Name)
                 .HasColumnName("Name")
                 .IsRequired();

            builder.Property(x => x.IsNullable)
                 .HasColumnName("IsNullable")
                 .IsRequired();

            builder.Property(x => x.PropertyTypeId)
                 .HasColumnName("PropertyTypeId")
                 .IsRequired();

            Relations(builder);

            builder.ToTable("Properties");
        }

        private static void Relations(EntityTypeBuilder<Property> builder)
        {
            builder.HasOne(x => x.PropertyType)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.PropertyTypeId);
        }
    }
}
