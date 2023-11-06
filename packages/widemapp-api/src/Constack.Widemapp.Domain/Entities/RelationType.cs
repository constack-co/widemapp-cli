using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class RelationType : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual ICollection<EntityRelation> EntityRelations { get; set; }
    }
}
