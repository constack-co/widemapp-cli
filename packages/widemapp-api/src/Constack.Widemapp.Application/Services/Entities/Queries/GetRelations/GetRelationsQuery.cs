using MediatR;

namespace Constack.Widemapp.Application.Services.Entities.Queries.GetRelations
{
    public class GetRelationsQuery : IRequest<IList<GetRelationsResponseModel>>
    {
    }
}
