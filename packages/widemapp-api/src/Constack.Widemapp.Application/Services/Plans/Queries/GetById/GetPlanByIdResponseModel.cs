namespace Constack.Widemapp.Application.Services.Plans.Queries.GetById
{
    public class GetPlanByIdResponseModel
    {
        public Guid PlanId { get; set; }
        public string PlanName { get; set; }
        public TemplateModel Template { get; set; }
        public EntityModel MainEntity { get; set; }
        public IList<string> PlanGroupNames { get; set; }
        public IList<PlanApisModel> PlanApis { get; set; }
        public IList<GenerationTypesModel> GenerationTypes { get; set; }
    }

    public class TemplateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class EntityModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<RelationsModel> Relations { get; set; }
        public IList<PropertyModel> Properties { get; set; }
    }

    public class RelationsModel
    {
        public Guid From { get; set; }
        public Guid To { get; set; }
        public Guid FromPort { get; set; }
        public Guid ToPort { get; set; }
    }

    public class PropertyModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PropertyTypeModel Type { get; set; }
        public bool IsNullable { get; set; }
    }

    public class PropertyTypeModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class PlanApisModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Endpoint { get; set; }
        public MethodModel Method { get; set; }
        public string GroupName { get; set; }
        public EntityModel Entity { get; set; } 
        public IList<ApiRequestsModel> ApiRequests { get; set; }
        public IList<ApiResponsesModel> ApiResponses { get; set; }
    }

    public class ApiRequestsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? IsNullable { get; set; }
    }

    public class ApiResponsesModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? IsNullable { get; set; }
    }

    public class MethodModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class GenerationTypesModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
