using MediatR;

namespace Constack.Widemapp.Application.Services.Templates.Queries.GetGroup
{
    public class GetFolderQuery : IRequest<GetFolderResponseModel>
    {
        public Guid Id { get; set; }
    }
}
