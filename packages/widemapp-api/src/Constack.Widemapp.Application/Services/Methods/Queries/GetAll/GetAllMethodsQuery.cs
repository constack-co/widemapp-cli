using MediatR;

namespace Constack.Widemapp.Application.Services.Methods.Queries.GetAll
{
    public class GetAllMethodsQuery : IRequest<IList<GetAllMethodsResponseModel>>
    {
    }
}
