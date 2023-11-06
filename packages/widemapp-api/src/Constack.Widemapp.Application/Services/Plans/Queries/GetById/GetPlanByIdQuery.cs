using MediatR;

namespace Constack.Widemapp.Application.Services.Plans.Queries.GetById
{
    public class GetPlanByIdQuery : IRequest<GetPlanByIdResponseModel>
    {
        public Guid Id { get; set; }
    }
}
