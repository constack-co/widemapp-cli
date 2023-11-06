using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Entities.Queries.Get
{
    public class GetEntitiesQueryHandler : IRequestHandler<GetEntitiesQuery, IList<GetEntitiesResponseModel>>
    {
        private readonly IAutomationDbContext _context;
        public GetEntitiesQueryHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<GetEntitiesResponseModel>> Handle(GetEntitiesQuery request, CancellationToken cancellationToken)
        {
            var microservice = await _context.Microservices.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId);

            return await _context.Entities
                .AsNoTracking()
                .Include(x => x.EntityProperties)
                .Where(x => x.MicroserviceId == microservice.Id)
                .Select(entity => new GetEntitiesResponseModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Properties = entity.EntityProperties.Select(property => new PropertyModel
                    {
                        Id = property.Property.Id,
                        Name = property.Property.Name,
                        IsNullable = property.Property.IsNullable,
                        Type = new PropertyTypeModel
                        {
                            Id = property.Property.PropertyType.Id,
                            Name = property.Property.PropertyType.Name
                        },
                    }).ToList(),
                    Inputs = entity.EntityProperties.Select(property => $"i{property.Property.Id}").ToList(),
                    Outputs = entity.EntityProperties.Select(property => $"o{property.Property.Id}").ToList(),
                }).ToListAsync(cancellationToken);
        }
    }
}
