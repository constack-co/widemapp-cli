using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class WebComponent : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string? Menu { get; set; }
        public string? Path { get; set; }
        public Guid? TemplateId { get; set; }

        public virtual ICollection<PlanApi> PlanApis { get; set; }
        public virtual Template Template { get; set; }
    }
}
