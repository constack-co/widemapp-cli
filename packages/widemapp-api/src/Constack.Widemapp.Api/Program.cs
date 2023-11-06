using Constack.Widemapp.Api.Extensions;
using Constack.Widemapp.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.RegisterServices(configuration)
    .AddControllers()
    .UseServices();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Run Migrations and Seed data
using var scope = app.Services.CreateScope();
using var dbContext = (AutomationDbContext)scope.ServiceProvider.GetRequiredService(typeof(AutomationDbContext));
await dbContext.Database.MigrateAsync();
await AutomationDbContextInitializer.Initialize(dbContext);

// Configure the HTTP request pipeline.
app.AddServices(configuration);
app.MapControllers();
app.Run();
