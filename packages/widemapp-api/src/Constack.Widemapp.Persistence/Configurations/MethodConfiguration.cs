using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class MethodConfiguration : IEntityTypeConfiguration<Method>
    {
        public void Configure(EntityTypeBuilder<Method> builder)
        {
            builder.Property(x => x.Name)
                 .HasColumnName("Name")
                 .IsRequired();

            builder.ToTable("Methods");
        }
    }
}
