using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("DefaultConnection")!);
})
    .UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapCarter();

app.Run();
