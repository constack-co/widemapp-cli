using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class Project : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        
        public virtual ICollection<Microservice> Microservices { get; set; }
        public virtual User User { get; set; }
    }
}
