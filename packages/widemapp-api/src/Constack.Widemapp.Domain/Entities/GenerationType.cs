using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class GenerationType : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual ICollection<PlanGenerationType> PlanGenerationTypes { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
