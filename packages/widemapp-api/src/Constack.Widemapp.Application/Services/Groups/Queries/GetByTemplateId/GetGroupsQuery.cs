using MediatR;

namespace Constack.Widemapp.Application.Services.Groups.Queries.GetByTemplateId
{
    public class GetGroupsQuery : IRequest<IList<GetGroupsResponseModel>>
    {
        public Guid TemplateId { get;set; }
    }
}
