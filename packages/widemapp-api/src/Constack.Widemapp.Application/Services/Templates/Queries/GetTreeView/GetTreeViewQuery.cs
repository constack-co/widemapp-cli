using MediatR;

namespace Constack.Widemapp.Application.Services.Templates.Queries.GetTreeView
{
    public class GetTreeViewQuery : IRequest<IList<GetTreeViewResponseModel>>
    {
        public Guid TemplateId { get; set; }
    }
}
