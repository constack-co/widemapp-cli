using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class RelationTypeConfiguration : IEntityTypeConfiguration<RelationType>
    {
        public void Configure(EntityTypeBuilder<RelationType> builder)
        {
            builder.Property(x => x.Name)
                 .HasColumnName("Name")
                 .IsRequired();

            builder.Property(x => x.Value)
                 .HasColumnName("Value")
                 .IsRequired();

            builder.ToTable("RelationTypes");
        }
    }
}
