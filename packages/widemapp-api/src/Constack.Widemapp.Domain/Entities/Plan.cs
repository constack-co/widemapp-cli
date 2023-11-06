using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class Plan : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public Guid? MicroserviceId { get; set; }
        public Guid UserId { get; set; }
        public Guid MainEntityId { get; set; }
        public Guid TemplateId { get; set; }

        public virtual Microservice Microservice { get; set; }
        public virtual Entity Entity { get; set; }
        public virtual ICollection<PlanApi> PlanApis { get; set; }
        public virtual ICollection<PlanGroup> PlanGroups { get; set; }
        public virtual ICollection<PlanGenerationType> PlanGenerationTypes { get; set; }
        public virtual Template Template { get; set; }
    }
}
