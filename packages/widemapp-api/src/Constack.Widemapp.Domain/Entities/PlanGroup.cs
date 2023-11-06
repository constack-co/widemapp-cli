using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class PlanGroup : BaseEntity<Guid>
    {
        public Guid PlanId { get; set; }
        public string GroupName { get; set; }

        public virtual Plan Plan { get; set; }
    }
}
