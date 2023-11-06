using MediatR;

namespace Constack.Widemapp.Application.Services.PropertyTypes.Queries.Get
{
    public class GetPropertyTypesQuery : IRequest<IList<GetPropertyTypesResponseModel>>
    {
    }
}
