using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class Template : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public Guid MicroserviceId { get; set; }

        public virtual ICollection<WebComponent> WebComponents { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
        public virtual Microservice Microservice { get; set; }

    }
}
