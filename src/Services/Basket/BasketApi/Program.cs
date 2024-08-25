using BasketApi.Data;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handlers;
using BuildingBlocks.Messaging.MassTransit;
using DiscountGrpc;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//application services
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddCarter();

//data services
builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("DefaultConnection")!);
    opt.Schema.For<ShoppingCart>()
        .Identity(x => x.UserName);
})
.UseLightweightSessions();
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddGrpcClient<DiscountService.DiscountServiceClient>(opt =>
{
    opt.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl")!);
})
    .ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    return handler;
});
//message broker
builder.Services.AddMessageBroker(builder.Configuration);

//cross-cutting services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();
app.MapCarter();
app.UseExceptionHandler(opt => { });

app.UseHealthChecks("/health", options: new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();
