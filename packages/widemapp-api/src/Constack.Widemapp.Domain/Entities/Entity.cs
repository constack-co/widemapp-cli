using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class Entity : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public Guid? MicroserviceId { get; set; }

        public virtual Microservice Microservice { get; set; }
        public virtual ICollection<EntityProperty> EntityProperties { get; set; }
        public virtual ICollection<EntityRelation> EntityRelations { get; set; }
        public virtual ICollection<Api> Apis { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
    }
}
