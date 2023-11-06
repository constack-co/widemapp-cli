using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class FileEditConfiguration : IEntityTypeConfiguration<FileEdit>
    {
        public void Configure(EntityTypeBuilder<FileEdit> builder)
        {
            builder.Property(x => x.Placeholder)
                 .HasColumnName("Placeholder")
                 .IsRequired();
            
            builder.Property(x => x.Content)
                 .HasColumnName("Content")
                 .IsRequired();

            builder.Property(x => x.FileId)
                 .HasColumnName("FileId")
                 .IsRequired();

            Relations(builder);

            builder.ToTable("FileEdits");
        }

        private static void Relations(EntityTypeBuilder<FileEdit> builder)
        {
            builder.HasOne(x => x.File)
                .WithMany(x => x.FileEdits)
                .HasForeignKey(x => x.FileId);
        }
    }
}
