using MediatR;

namespace Constack.Widemapp.Application.Services.Templates.Queries.GetTreeByGroupId
{
    public class GetTreeByGroupIdQuery : IRequest<IList<GetTreeByGroupIdResponseModel>>
    {
        public Guid GroupId { get; set; }
    }
}
