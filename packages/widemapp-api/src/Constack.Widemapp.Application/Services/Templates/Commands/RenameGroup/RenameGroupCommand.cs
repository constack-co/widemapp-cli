using MediatR;

namespace Constack.Widemapp.Application.Services.Templates.Commands.RenameGroup
{
    public class RenameGroupCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
