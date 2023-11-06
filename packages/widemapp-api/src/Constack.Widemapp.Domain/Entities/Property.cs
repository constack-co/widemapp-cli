using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class Property : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public bool IsNullable { get; set; }
        public Guid PropertyTypeId { get; set; }

        public virtual PropertyType PropertyType { get; set; }
        public virtual ICollection<EntityProperty> EntityProperties { get; set; }
        public virtual ICollection<EntityRelation> EntityRelations { get; set; }
    }
}
