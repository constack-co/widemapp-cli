using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class MicroserviceConfiguration : IEntityTypeConfiguration<Microservice>
    {
        public void Configure(EntityTypeBuilder<Microservice> builder)
        {
            builder.Property(x => x.Name)
                 .HasColumnName("Name")
                 .IsRequired();

            builder.Property(x => x.ProjectId)
                 .HasColumnName("ProjectId")
                 .IsRequired();

            Relations(builder);

            builder.ToTable("Microservices");
        }

        private static void Relations(EntityTypeBuilder<Microservice> builder)
        {
            builder.HasOne(x => x.Project)
                .WithMany(x => x.Microservices)
                .HasForeignKey(x => x.ProjectId);
        }
    }
}
