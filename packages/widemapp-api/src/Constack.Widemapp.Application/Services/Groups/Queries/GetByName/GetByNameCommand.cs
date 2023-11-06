using MediatR;

namespace Constack.Widemapp.Application.Services.Groups.Queries.GetByName
{
    public class GetByNameCommand : IRequest<IList<GetByNameCommandResponseModel>>
    {
        public string GroupName { get; set;}
        public Guid TemplateId { get; set; }
    }
}
