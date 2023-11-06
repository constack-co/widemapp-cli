using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class PropertyType : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
