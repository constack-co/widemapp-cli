using MediatR;

namespace Constack.Widemapp.Application.Services.Plans.Queries.GetGenerationTypes
{
    public class GetGenerationTypesQuery : IRequest<IList<GetGenerationTypesResponseModel>>
    {
    }
}
