using MediatR;

namespace Constack.Widemapp.Application.Services.Plans.Commands.Finish
{
    public class FinishPlanCommand : IRequest<FinishPlanResponseModel>
    {
        public string PlanName { get; set; }
        public Guid MainEntityId { get; set; }
        public Guid TemplateId { get; set; }
        public IList<Guid> GenerateTypeIds { get; set; }
        public IList<string>? PlanGroupNames { get; set; }
        public IList<ApiRequestModel> Apis { get; set; }
    }

    public class ApiRequestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid MethodId { get; set; }
        public string Endpoint { get; set; }
        public string GroupName { get; set; }
        public Guid EntityId { get; set; }
        public IList<ApiEntityRequests> ApiRequests { get; set; }
        public IList<ApiEntityResponses> ApiResponses { get; set; }
    }

    public class ApiEntityRequests
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool? IsNullable { get; set; }
    }

    public class ApiEntityResponses
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool? IsNullable { get; set; }
    }
}
