using Application.Activities.Queries;
using Application.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Register controllers service to handle API requests
builder.Services.AddControllers();

// Register your database context (AppDbContext) with dependency injection,
// configuring it to use SQLite with the connection string named "DefaultConnection"
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    // Use SQLite database with connection string from configuration
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Set up CORS (Cross-Origin Resource Sharing) policy named "AllowFrontend"
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            // Allow any HTTP headers
            .AllowAnyHeader()
            // Allow any HTTP methods (GET, POST, etc.)
            .AllowAnyMethod()
            // Restrict origins to these specific URLs (your frontend app)
            .WithOrigins("http://localhost:3000", "https://localhost:3000"));
});

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetActivities.Handler>()); //#
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly); //#
// Build the application with the configured services
var app = builder.Build();

// Enable CORS middleware with the "AllowFrontend" policy
app.UseCors("AllowFrontend");

// Map HTTP requests to controller endpoints
app.MapControllers();

// Create a scope for resolving scoped services during startup
using var scope = app.Services.CreateScope();
// Get the service provider within this scope
var services = scope.ServiceProvider;

try
{
    // Get the database context from DI
    var context = services.GetRequiredService<AppDbContext>();
    // Apply pending migrations to the database (update schema if needed)
    await context.Database.MigrateAsync();
    // Seed initial data into the database
    await DbInitializer.SeedData(context);
}
catch (Exception ex)
{
    // If migration or seeding fails, log the error
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
    throw; // rethrow exception after logging
}

app.Run();
