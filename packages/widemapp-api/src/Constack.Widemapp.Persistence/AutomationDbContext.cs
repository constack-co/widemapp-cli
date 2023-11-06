using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Domain.Entities;
using Constack.Widemapp.Persistence.Configurations.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Persistence
{
    public class AutomationDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IAutomationDbContext
    {
        public DbSet<Api> Apis { get; set; }
        public DbSet<ApiRequest> ApiRequests { get; set; }
        public DbSet<ApiResponse> ApiResponses { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<EntityProperty> EntityProperties { get; set; }
        public DbSet<EntityRelation> EntityRelations { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<GenerationType> GenerationTypes { get; set; }
        public DbSet<FileEdit> FileEdits { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Method> Methods { get; set; }
        public DbSet<Microservice> Microservices { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanApi> PlanApis { get; set; }
        public DbSet<PlanGroup> PlanGroups { get; set; }
        public DbSet<PlanGenerationType> PlanGenerationTypes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<RelationType> RelationTypes { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<WebComponent> WebComponents { get; set; }

        public AutomationDbContext(DbContextOptions<AutomationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType, builder => builder.UseBaseConfigurations(entityType.ClrType));

                var table = entityType.GetTableName();
                if (table.StartsWith("AspNet"))
                    entityType.SetTableName(table.Substring(6));
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AutomationDbContext).Assembly);
        }
    }
}
