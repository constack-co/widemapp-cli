using Constack.Widemapp.Domain.Entities;
using MediatR;

namespace Constack.Widemapp.Application.Services.Projects.Commands.Add
{
    public class AddProjectCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public Project AddProject(Guid userId)
        {
            return new Project()
            {
                Id = Guid.NewGuid(),
                Name = Name,
                UserId = userId
            };
        }
    }
}
