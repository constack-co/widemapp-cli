using Constack.Widemapp.Common.Extensions;
using Constack.Widemapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Persistence
{
    public class AutomationDbContextInitializer
    {
        public static async Task Initialize(AutomationDbContext context)
        {
            await SeedRoles(context);

            await SeedUsers(context);

            await SeedUserRoles(context);

            await SeedRelationTypes(context);

            await SeedPropertyTypes(context);

            await SeedGenerationTypes(context);

            await SeedMethodTypes(context);
        }

        private static async Task SeedUsers(AutomationDbContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var users = (await ReadJson("users.json")).Deserialize<IList<User>>();

            await context.Users.AddRangeAsync(users);

            await context.SaveChangesAsync();
        }

        private static async Task SeedRoles(AutomationDbContext context)
        {
            if (await context.Roles.AnyAsync()) return;

            var roles = (await ReadJson("roles.json")).Deserialize<IList<Role>>();

            await context.Roles.AddRangeAsync(roles);

            await context.SaveChangesAsync();
        }

        private static async Task SeedUserRoles(AutomationDbContext context)
        {
            if (await context.UserRoles.AnyAsync()) return;

            var userRoles = (await ReadJson("user-roles.json")).Deserialize<IList<UserRole>>();

            await context.UserRoles.AddRangeAsync(userRoles);

            await context.SaveChangesAsync();
        }

        private static async Task SeedRelationTypes(AutomationDbContext context)
        {
            if (await context.RelationTypes.AnyAsync()) return;

            var relationTypes = (await ReadJson("relation-types.json")).Deserialize<IList<RelationType>>();

            await context.RelationTypes.AddRangeAsync(relationTypes);

            await context.SaveChangesAsync();
        }

        private static async Task SeedPropertyTypes(AutomationDbContext context)
        {
            if (await context.PropertyTypes.AnyAsync()) return;

            var propertyTypes = (await ReadJson("property-types.json")).Deserialize<IList<PropertyType>>();

            await context.PropertyTypes.AddRangeAsync(propertyTypes);

            await context.SaveChangesAsync();
        }
        
        private static async Task SeedGenerationTypes(AutomationDbContext context)
        {
            if (await context.GenerationTypes.AnyAsync()) return;

            var generationTypes = (await ReadJson("generation-types.json")).Deserialize<IList<GenerationType>>();

            await context.GenerationTypes.AddRangeAsync(generationTypes);

            await context.SaveChangesAsync();
        }
        
        private static async Task SeedMethodTypes(AutomationDbContext context)
        {
            if (await context.Methods.AnyAsync()) return;

            var methods = (await ReadJson("method-types.json")).Deserialize<IList<Method>>();

            await context.Methods.AddRangeAsync(methods);

            await context.SaveChangesAsync();
        }

        private static async Task<string> ReadJson(string filename)
        {
            var assembly = typeof(AutomationDbContext).Assembly;

            var resources = assembly.GetManifestResourceNames();

            using Stream stream = assembly.GetManifestResourceStream(resources.First(x => x.Contains(filename)));

            using StreamReader reader = new(stream);

            return await reader.ReadToEndAsync();
        }
    }
}
