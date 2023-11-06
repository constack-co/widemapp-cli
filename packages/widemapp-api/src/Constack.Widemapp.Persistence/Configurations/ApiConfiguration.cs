using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class ApiConfiguration : IEntityTypeConfiguration<Api>
    {
        public void Configure(EntityTypeBuilder<Api> builder)
        {
            builder.Property(x => x.Name)
                 .HasColumnName("Name")
                 .IsRequired();

            builder.Property(x => x.Description)
                 .HasColumnName("Description")
                 .IsRequired(false);

            builder.Property(x => x.Endpoint)
                 .HasColumnName("Endpoint")
                 .IsRequired();

            builder.Property(x => x.MethodId)
                 .HasColumnName("MethodId")
                 .IsRequired();

            builder.Property(x => x.GroupName)
                .HasColumnName("GroupName")
                .IsRequired();

            builder.Property(x => x.EntityId)
                .HasColumnName("EntityId")
                .IsRequired();

            Relations(builder);

            builder.ToTable("Apis");
        }

        private static void Relations(EntityTypeBuilder<Api> builder)
        {
            builder.HasOne(x => x.Method)
                .WithMany(x => x.Apis)
                .HasForeignKey(x => x.MethodId);

            builder.HasOne(x => x.Entity)
                .WithMany(x => x.Apis)
                .HasForeignKey(x => x.EntityId);
        }
    }
}
