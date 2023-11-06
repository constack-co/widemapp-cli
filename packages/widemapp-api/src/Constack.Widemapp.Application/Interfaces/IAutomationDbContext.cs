using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Interfaces
{
    public interface IAutomationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Api> Apis { get; set; }
        DbSet<ApiRequest> ApiRequests { get; set; }
        DbSet<ApiResponse> ApiResponses { get; set; }
        DbSet<Entity> Entities { get; set; }
        DbSet<EntityProperty> EntityProperties { get; set; }
        DbSet<EntityRelation> EntityRelations { get; set; }
        DbSet<Domain.Entities.File> Files { get; set; }
        DbSet<FileEdit> FileEdits { get; set; }
        DbSet<GenerationType> GenerationTypes { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<Method> Methods { get; set; }
        DbSet<Microservice> Microservices { get; set; }
        DbSet<Plan> Plans { get; set; }
        DbSet<PlanApi> PlanApis { get; set; }
        DbSet<PlanGroup> PlanGroups { get; set; }
        DbSet<PlanGenerationType> PlanGenerationTypes { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Property> Properties { get; set; }
        DbSet<PropertyType> PropertyTypes { get; set; }
        DbSet<RelationType> RelationTypes { get; set; }
        DbSet<Template> Templates { get; set; }
        DbSet<WebComponent> WebComponents { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
