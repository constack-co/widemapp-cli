using MediatR;

namespace Constack.Widemapp.Application.Services.Templates.Queries.GetAll
{
    public class GetAllTemplatesQuery : IRequest<IList<GetAllTemplatesResponseModel>>
    {
        public Guid ProjectId { get; set; }
    }
}
