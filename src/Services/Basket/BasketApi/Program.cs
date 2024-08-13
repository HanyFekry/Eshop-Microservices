using BasketApi.Data;
using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddCarter();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();
app.MapCarter();

app.Run();
