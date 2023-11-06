using MediatR;

namespace Constack.Widemapp.Application.Services.Entities.Queries.Get
{
    public class GetEntitiesQuery : IRequest<IList<GetEntitiesResponseModel>>
    {
        public Guid ProjectId { get; set; }
    }
}
