using MassTransit;
using EventApi.Domain;
using System.Threading.Tasks;

namespace EventApi.Infrastructure;

public interface IEventPublisher
{
    Task PublishAsync(Event evt);
}

public class EventPublisher : IEventPublisher
{
    private readonly IBus _bus;
    public EventPublisher(IBus bus) => _bus = bus;

    public async Task PublishAsync(Domain.Event evt)
    {
        await _bus.Publish(new { evt.Id, evt.Type, evt.Payload }, context => context.SetRoutingKey("event-queue"));
    }
}