using MediatR;

namespace Constack.Widemapp.Application.Services.Templates.Commands.DeleteFileOrGroup
{
    public class DeleteFileOrGroupCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
    }
}
