using MassTransit;
using EventApi.Domain;
using System.Threading.Tasks;

namespace EventApi.Infrastructure;

public interface IEventPublisher
{
    Task PublishAsync(Domain.Event evt);
}