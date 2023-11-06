using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class EntityRelationConfiguration : IEntityTypeConfiguration<EntityRelation>
    {
        public void Configure(EntityTypeBuilder<EntityRelation> builder)
        {
            builder.Property(x => x.EntityFromId)
                 .HasColumnName("EntityFromId")
                 .IsRequired();

            builder.Property(x => x.EntityToId)
                 .HasColumnName("EntityToId")
                 .IsRequired();

            builder.Property(x => x.PropertyFromId)
                 .HasColumnName("PropertyFromId")
                 .IsRequired();

            builder.Property(x => x.PropertyToId)
                 .HasColumnName("PropertyToId")
                 .IsRequired();
            
            builder.Property(x => x.RelationTypeId)
                 .HasColumnName("RelationTypeId")
                 .IsRequired();

            Relations(builder);

            builder.ToTable("EntityRelations");
        }

        private static void Relations(EntityTypeBuilder<EntityRelation> builder)
        {
            builder.HasOne(x => x.Entity)
                .WithMany(x => x.EntityRelations)
                .HasForeignKey(x => x.EntityFromId);

            builder.HasOne(x => x.Entity)
                .WithMany(x => x.EntityRelations)
                .HasForeignKey(x => x.EntityToId);

            builder.HasOne(x => x.Property)
                .WithMany(x => x.EntityRelations)
                .HasForeignKey(x => x.PropertyFromId);

            builder.HasOne(x => x.Property)
                .WithMany(x => x.EntityRelations)
                .HasForeignKey(x => x.PropertyToId);
            
            builder.HasOne(x => x.RelationType)
                .WithMany(x => x.EntityRelations)
                .HasForeignKey(x => x.RelationTypeId);
        }
    }
}
