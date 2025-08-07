using Application.Activities.Queries;
using Application.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:3000", "https://localhost:3000"));
});

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetActivities.Handler>());
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

var app = builder.Build();

app.UseCors("AllowFrontend");

app.MapControllers();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await DbInitializer.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
    throw;
}

app.Run();
