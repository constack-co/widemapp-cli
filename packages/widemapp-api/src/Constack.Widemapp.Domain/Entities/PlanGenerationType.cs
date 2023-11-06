using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class PlanGenerationType : BaseEntity<Guid>
    {
        public Guid PlanId { get; set; }
        public Guid GenerationTypeId { get; set; }

        public virtual Plan Plan { get; set; }
        public virtual GenerationType GenerationType { get; set; }
    }
}
