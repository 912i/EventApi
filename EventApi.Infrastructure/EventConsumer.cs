using MassTransit;
using Microsoft.AspNetCore.SignalR;


namespace EventApi.Infrastructure;

public class EventConsumer : IConsumer<EventMessage>
{
    private readonly IHubContext<EventHub> _hubContext;

    public EventConsumer(IHubContext<EventHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Consume(ConsumeContext<EventMessage> context)
    {
        var message = context.Message;
        await _hubContext.Clients.All.SendAsync("ReceiveEvent", message.Type, message.Payload);
    }
}