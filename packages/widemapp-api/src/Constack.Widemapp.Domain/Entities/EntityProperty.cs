using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class EntityProperty : BaseEntity<Guid>
    {
        public Guid EntityId { get; set; }
        public Guid PropertyId { get; set; }

        public virtual Entity Entity { get; set; }
        public virtual Property Property { get; set; }
    }
}
