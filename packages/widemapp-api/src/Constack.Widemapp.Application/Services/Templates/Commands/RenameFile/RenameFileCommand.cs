using MediatR;

namespace Constack.Widemapp.Application.Services.Templates.Commands.RenameFile
{
    public class RenameFileCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
