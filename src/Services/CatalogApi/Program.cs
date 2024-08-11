using BuildingBlocks.Behaviors;
using BuildingBlocks.Behaviours;
using BuildingBlocks.Exceptions.Handlers;
using CatalogApi.Data;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var assembly = typeof(Program).Assembly;
//register MediatR for CQRS
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
//register fluent validation
builder.Services.AddValidatorsFromAssembly(assembly);

//add Carter for minimal API
builder.Services.AddCarter();

//add Marten to use Postgresql as document DB
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("DefaultConnection")!);
})
    //.InitializeWith(new InitialData(CatalogInitialdata.products))
    .UseLightweightSessions();
if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialdata>();

//add custom exception handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//add health checks for postge sql
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);


var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
