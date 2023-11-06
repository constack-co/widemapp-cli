using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class PlanApi : BaseEntity<Guid>
    {
        public Guid? PlanId { get; set; }
        public Guid? ApiId { get; set; }
        public Guid? WebComponentId { get; set; }

        public virtual Api Api { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual WebComponent WebComponent { get; set; }
    }
}
