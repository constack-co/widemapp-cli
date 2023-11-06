using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class Api : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Endpoint { get; set; }
        public Guid MethodId { get; set; }
        public string GroupName { get; set; }
        public Guid EntityId { get; set; }

        public virtual ICollection<PlanApi> PlanApis { get; set; }
        public virtual ICollection<ApiRequest> ApiRequests { get; set; }
        public virtual ICollection<ApiResponse> ApiResponses { get; set; }
        public virtual Method Method { get; set; }
        public virtual Entity Entity { get; set; }
    }
}
