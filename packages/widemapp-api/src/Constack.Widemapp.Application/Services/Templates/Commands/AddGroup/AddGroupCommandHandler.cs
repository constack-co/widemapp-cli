using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Domain.Entities;
using Constack.Widemapp.Domain.Enums;
using MediatR;

namespace Constack.Widemapp.Application.Services.Templates.Commands.AddGroup
{
    public class AddGroupCommandHandler : IRequestHandler<AddGroupCommand, AddGroupResponseModel>
    {
        private readonly IAutomationDbContext _context;
        public AddGroupCommandHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<AddGroupResponseModel> Handle(AddGroupCommand request, CancellationToken cancellationToken)
        {
            var group = request.AddGroup();
            
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return new AddGroupResponseModel
            {
                Id = group.Id
            };
        }
    }
}
