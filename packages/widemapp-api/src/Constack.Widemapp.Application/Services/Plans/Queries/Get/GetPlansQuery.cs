using MediatR;

namespace Constack.Widemapp.Application.Services.Plans.Queries.Get
{
    public class GetPlansQuery : IRequest<IList<GetPlansResponseModel>>
    {
    }
}
