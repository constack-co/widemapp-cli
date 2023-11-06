using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Common.Helpers;
using Constack.Widemapp.Domain.Entities;
using MediatR;

namespace Constack.Widemapp.Application.Services.Plans.Commands.Finish
{
    public class FinishPlanCommandHandler : IRequestHandler<FinishPlanCommand, FinishPlanResponseModel>
    {
        private readonly IAutomationDbContext _context;
        public FinishPlanCommandHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<FinishPlanResponseModel> Handle(FinishPlanCommand request, CancellationToken cancellationToken)
        {
            var plan = new Plan
            {
                Id = Guid.NewGuid(),
                Name = request.PlanName,
                MainEntityId = request.MainEntityId,
                TemplateId = request.TemplateId,
                UserId = AuthHelper.AuthId
            };

            IList<PlanGenerationType> planGenerationTypes = new List<PlanGenerationType>();
            foreach (var generateTypeId in request.GenerateTypeIds)
            {
                planGenerationTypes.Add(new PlanGenerationType
                {
                    Id = Guid.NewGuid(),
                    PlanId = plan.Id,
                    GenerationTypeId = generateTypeId
                });
            }

            IList<PlanGroup> planGroups = new List<PlanGroup>();
            foreach(var planGroupName in request.PlanGroupNames)
            {
                planGroups.Add(new PlanGroup
                {
                    Id = Guid.NewGuid(),
                    PlanId = plan.Id,
                    GroupName = planGroupName
                });
            }

            IList<Api> apis = new List<Api>();
            IList<ApiRequest> apiRequests = new List<ApiRequest>();
            IList<ApiResponse> apiResponses = new List<ApiResponse>();
            foreach (var api in request.Apis)
            {
                apis.Add(new Api
                {
                    Id = api.Id,
                    Name = api.Name,
                    MethodId = api.MethodId,
                    Endpoint = api.Endpoint,
                    GroupName = api.GroupName,
                    EntityId = api.EntityId
                });

                foreach(var apiRequest in api.ApiRequests)
                {
                    apiRequests.Add(new ApiRequest
                    {
                        Id = Guid.NewGuid(),
                        Name = apiRequest.Name,
                        Type = apiRequest.Type,
                        IsNullable = apiRequest.IsNullable,
                        ApiId = api.Id
                    });
                }
                
                foreach(var apiResponse in api.ApiResponses)
                {
                    apiResponses.Add(new ApiResponse
                    {
                        Id = Guid.NewGuid(),
                        Name = apiResponse.Name,
                        Type = apiResponse.Type,
                        IsNullable = apiResponse.IsNullable,
                        ApiId = api.Id
                    });
                }
                
            }

            IList<PlanApi> planApis = new List<PlanApi>();

            IList<ApiResonseModel> apiResponseModel = new List<ApiResonseModel>();

            foreach (var api in apis)
            {
                var planApi = new PlanApi
                {
                    Id = Guid.NewGuid(),
                    ApiId = api.Id,
                    PlanId = plan.Id
                };

                planApis.Add(planApi);

                apiResponseModel.Add(new ApiResonseModel
                {
                    ApiId = api.Id,
                    PlanApiId = planApi.Id
                });
            }

            await _context.Plans.AddAsync(plan);
            await _context.Apis.AddRangeAsync(apis);
            await _context.PlanApis.AddRangeAsync(planApis);
            await _context.PlanGroups.AddRangeAsync(planGroups);
            await _context.PlanGenerationTypes.AddRangeAsync(planGenerationTypes);
            await _context.ApiRequests.AddRangeAsync(apiRequests);
            await _context.ApiResponses.AddRangeAsync(apiResponses);

            await _context.SaveChangesAsync();

            return new FinishPlanResponseModel
            {
                PlanId = plan.Id,
                Apis = apiResponseModel
            };
        }
    }
}
