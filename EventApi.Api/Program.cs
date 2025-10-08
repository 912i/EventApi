using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using MassTransit;
using MediatR;
using EventApi.Infrastructure;
using EventApi.Application;
using EventApi.Domain;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddDbContext<AppDbContext>(o => o.UseMySql(builder.Configuration.GetConnectionString("MariaDB"), new MySqlServerVersion("8.0")));
builder.Services.AddStackExchangeRedisCache(o => o.Configuration = builder.Configuration.GetConnectionString("Redis"));
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<EventConsumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
        cfg.ReceiveEndpoint("event-queue", e =>
        {
            e.ConfigureConsumer<EventConsumer>(ctx);
        });
    });
});
builder.Services.AddSignalR();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateEventCommand).Assembly));
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventPublisher, EventPublisher>();
builder.Services.AddScoped<EventCountService>();
builder.Services.AddScoped<IEventAnalyticsService, EventAnalyticsService>();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
//app.UseSwagger();
//app.UseSwaggerUI();
app.MapPost("/events", async (CreateEventCommand cmd, ISender mediator) => Results.Ok(await mediator.Send(cmd)));
app.MapGet("/events/{type}", async (string type, ISender mediator) => Results.Ok(await mediator.Send(new GetEventsByTypeQuery { Type = type })));
app.MapGet("/events/{type}/count", async (string type, EventCountService svc) => Results.Ok(await svc.GetEventCountAsync(type)));
app.MapHub<EventHub>("/hub");

app.Run();

public class EventHub : Hub
{
    public async Task SendEvent(string type, string payload)
    {
        await Clients.All.SendAsync("ReceiveEvent", type, payload);
    }
}