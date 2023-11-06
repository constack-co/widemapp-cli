using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class Microservice : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public Guid ProjectId { get; set; }

        public virtual ICollection<Entity> Entities { get; set; }
        public virtual ICollection<Template> Templates { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
        public virtual Project Project { get; set; }
    }
}
