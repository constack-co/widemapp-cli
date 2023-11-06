using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.Property(x => x.Name)
                 .HasColumnName("Name")
                 .IsRequired();

            builder.Property(x => x.MicroserviceId)
                 .HasColumnName("MicroserviceId")
                 .IsRequired(false);

            Relations(builder);

            builder.ToTable("Entities");
        }

        private static void Relations(EntityTypeBuilder<Entity> builder)
        {
            builder.HasOne(x => x.Microservice)
                .WithMany(x => x.Entities)
                .HasForeignKey(x => x.MicroserviceId);
        }
    }
}
