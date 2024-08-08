using BuildingBlocks.Behaviors;
using BuildingBlocks.Behaviours;
using BuildingBlocks.Exceptions.Handlers;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

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
//add Marten to use Postgre sql as document DB
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("DefaultConnection")!);
})
    .UseLightweightSessions();
//add custom exception handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
