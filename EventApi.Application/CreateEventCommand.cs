using MediatR;
using EventApi.Domain;
using EventApi.Infrastructure; // Ajouté pour IEventPublisher et EventAnalyticsService

namespace EventApi.Application;

public class CreateEventCommand : IRequest<Guid>
{
    public string Type { get; set; } = null!;
    public string? Payload { get; set; }
}

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepository _repo;
    private readonly IEventPublisher _publisher;
    private readonly IEventAnalyticsService _analytics;

    public CreateEventCommandHandler(IEventRepository repo, IEventPublisher publisher, IEventAnalyticsService analytics)
    {
        _repo = repo;
        _publisher = publisher;
        _analytics = analytics;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken ct)
    {
        var evt = new Event(request.Type, request.Payload);
        await _repo.AddAsync(evt, ct); // MariaDB
        await _analytics.InsertEventAsync(evt); // ClickHouse
        await _publisher.PublishAsync(evt); // RabbitMQ
        return evt.Id;
    }
}