using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(x => x.Name)
                 .HasColumnName("Name")
                 .IsRequired();

            builder.Property(x => x.Type)
                 .HasColumnName("Type")
                 .IsRequired();

            builder.Property(x => x.TemplateId)
                 .HasColumnName("TemplateId")
                 .IsRequired();

            builder.Property(x => x.GenerationTypeId)
                .HasColumnName("GenerationTypeId")
                .IsRequired(false);

            builder.Property(x => x.GroupId)
                .HasColumnName("GroupId")
                .IsRequired(false);

            Relations(builder);

            builder.ToTable("Groups");
        }

        private static void Relations(EntityTypeBuilder<Group> builder)
        {
            builder.HasOne(x => x.Template)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.TemplateId);

            builder.HasOne(x => x.GroupOwn)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.GroupId);

            builder.HasOne(x => x.GenerationType)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.GenerationTypeId);
        }
    }
}
