using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Constack.Widemapp.Persistence.Configurations.Base
{
    public static class BaseEntityConfiguration
    {
        public static void UseBaseConfigurations(this EntityTypeBuilder builder, Type type)
        {
            IList<BaseEntityModel> props = BaseEntityModel.GetList();

            foreach (var value in props)
            {

                if (type.HasProperty(value.Property))
                {
                    if (value.IsKey)
                        builder.HasKey(value.Property);

                    builder.Property(value.Property)
                        .HasColumnName(value.Value)
                        .IsRequired(value.IsRequired);
                }
            }
        }

        public static bool HasProperty(this Type type, string propertyName)
        {
            return type.GetProperty(propertyName) != null;
        }
    }
}
