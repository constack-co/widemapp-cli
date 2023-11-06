using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations
{
    public class ApiResponseConfiguration : IEntityTypeConfiguration<ApiResponse>
    {
        public void Configure(EntityTypeBuilder<ApiResponse> builder)
        {
            builder.Property(x => x.Name)
                 .HasColumnName("Name")
                 .IsRequired();

            builder.Property(x => x.Type)
                 .HasColumnName("Type")
                 .IsRequired();

            builder.Property(x => x.IsNullable)
                 .HasColumnName("IsNullable")
                 .IsRequired(false);

            builder.Property(x => x.ApiId)
                 .HasColumnName("ApiId")
                 .IsRequired();

            Relations(builder);

            builder.ToTable("ApiResponses");
        }

        private static void Relations(EntityTypeBuilder<ApiResponse> builder)
        {
            builder.HasOne(x => x.Api)
                .WithMany(x => x.ApiResponses)
                .HasForeignKey(x => x.ApiId);
        }
    }
}
