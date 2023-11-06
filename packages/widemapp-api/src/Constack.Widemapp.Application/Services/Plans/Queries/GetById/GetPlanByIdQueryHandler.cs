using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constack.Widemapp.Application.Services.Plans.Queries.GetById
{
    public class GetPlanByIdQueryHandler : IRequestHandler<GetPlanByIdQuery, GetPlanByIdResponseModel>
    {
        private readonly IAutomationDbContext _context;
        public GetPlanByIdQueryHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetPlanByIdResponseModel> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var plan = await _context.Plans
                .Include(x => x.Template)
                .Include(x => x.Entity)
                .ThenInclude(x => x.EntityRelations)
                .Include(x => x.Entity)
                .ThenInclude(x => x.EntityProperties)
                .ThenInclude(x => x.Property)
                .ThenInclude(x => x.PropertyType)
                .Include(x => x.PlanGroups)
                .Include(x => x.PlanApis)
                .ThenInclude(x => x.Api)
                .ThenInclude(x => x.Method)
                .Include(x => x.PlanApis)
                .ThenInclude(x => x.Api)
                .Include(x => x.PlanApis)
                .ThenInclude(x => x.Api)
                .ThenInclude(x => x.Entity)
                .Include(x => x.PlanApis)
                .ThenInclude(x => x.Api)
                .ThenInclude(x => x.Entity)
                .ThenInclude(x => x.EntityRelations)
                .Include(x => x.PlanApis)
                .ThenInclude(x => x.Api)
                .ThenInclude(x => x.Entity)
                .ThenInclude(x => x.EntityProperties)
                .ThenInclude(x => x.Property)
                .ThenInclude(x => x.PropertyType)
                .Include(x => x.PlanApis)
                .ThenInclude(x => x.Api)
                .ThenInclude(x => x.ApiRequests)
                .Include(x => x.PlanApis)
                .ThenInclude(x => x.Api)
                .ThenInclude(x => x.ApiResponses)
                .Include(x => x.PlanGenerationTypes)
                .ThenInclude(x => x.GenerationType)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            
            return new GetPlanByIdResponseModel {
                PlanId = plan.Id,
                PlanName = plan.Name,
                Template = new TemplateModel
                {
                    Id = plan.TemplateId,
                    Name = plan.Template.Name
                },
                MainEntity = new EntityModel
                {
                    Id = plan.Entity.Id,
                    Name = plan.Entity.Name,
                    Relations = plan.Entity.EntityRelations.Select(relation => new RelationsModel
                    {
                        From = relation.EntityFromId,
                        To = relation.EntityToId,
                        FromPort = relation.PropertyFromId,
                        ToPort = relation.PropertyToId,
                    }).ToList(),
                    Properties = plan.Entity.EntityProperties.Select(property => new PropertyModel
                    {
                        Id = property.Property.Id,
                        Name = property.Property.Name,
                        IsNullable = property.Property.IsNullable,
                        Type = new PropertyTypeModel
                        {
                            Name = property.Property.PropertyType.Name
                        },
                    }).ToList(),
                },
                PlanGroupNames = plan.PlanGroups.Select(planGroup => new string(planGroup.GroupName)).ToList(),
                PlanApis = plan.PlanApis.Select(planApi => new PlanApisModel
                {
                    Id = planApi.Api.Id,
                    Name= planApi.Api.Name,
                    Endpoint = planApi.Api.Endpoint,
                    Method = new MethodModel
                    {
                        Id = planApi.Api.Method.Id,
                        Name = planApi.Api.Method.Name
                    },
                    GroupName = planApi.Api.GroupName,
                    Entity = new EntityModel
                    {
                        Id = planApi.Api.Entity.Id,
                        Name = planApi.Api.Entity.Name,
                        Relations = planApi.Api.Entity.EntityRelations.Select(relation => new RelationsModel
                        {
                            From = relation.EntityFromId,
                            To = relation.EntityToId,
                            FromPort = relation.PropertyFromId,
                            ToPort = relation.PropertyToId,
                        }).ToList(),
                        Properties = planApi.Api.Entity.EntityProperties.Select(property => new PropertyModel
                        {
                            Id = property.Property.Id,
                            Name = property.Property.Name,
                            IsNullable = property.Property.IsNullable,
                            Type = new PropertyTypeModel
                            {
                                Name = property.Property.PropertyType.Name
                            },
                        }).ToList(),
                    },
                    ApiRequests = planApi.Api.ApiRequests.Select(apiRequest => new ApiRequestsModel
                    {
                        Id = apiRequest.Id,
                        Name = apiRequest.Name,
                        IsNullable = apiRequest.IsNullable
                    }).ToList(),
                    ApiResponses = planApi.Api.ApiResponses.Select(apiResponse => new ApiResponsesModel
                    {
                        Id = apiResponse.Id,
                        Name = apiResponse.Name,
                        IsNullable = apiResponse.IsNullable
                    }).ToList()
                }).ToList(),
                GenerationTypes = plan.PlanGenerationTypes.Select(generationType => new GenerationTypesModel
                {
                    Id = generationType.GenerationType.Id,
                    Name = generationType.GenerationType.Name,
                    Value = generationType.GenerationType.Value
                }).ToList()
            };
        }
    }
}
