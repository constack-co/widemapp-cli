using MediatR;

namespace Constack.Widemapp.Application.Services.Templates.Queries.GetFile
{
    public class GetFileQuery : IRequest<GetFileResponseModel>
    {
        public Guid Id { get; set; }
    }
}
