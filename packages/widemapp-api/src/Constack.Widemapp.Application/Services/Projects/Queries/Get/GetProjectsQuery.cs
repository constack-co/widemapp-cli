using MediatR;

namespace Constack.Widemapp.Application.Services.Projects.Queries.Get
{
    public class GetProjectsQuery : IRequest<IList<GetProjectsResponseModel>>
    {
    }
}
