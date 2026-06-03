using Identity.Api;
using Identity.Application;
using Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// register application, infrastructure and api services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

// Use controllers
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
