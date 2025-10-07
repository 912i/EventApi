using MediatR;
using EventApi.Domain;

namespace EventApi.Application;

public class GetEventsByTypeQuery : IRequest<List<EventDto>>
{
    public string Type { get; set; }
}

public class GetEventsByTypeQueryHandler : IRequestHandler<GetEventsByTypeQuery, List<EventDto>>
{
    private readonly IEventRepository _repo;

    public GetEventsByTypeQueryHandler(IEventRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<EventDto>> Handle(GetEventsByTypeQuery request, CancellationToken ct)
    {
        var events = await _repo.GetByTypeAsync(request.Type, ct);
        return events.Select(e => new EventDto
        {
            Id = e.Id,
            Type = e.Type,
            Timestamp = e.Timestamp,
            Payload = e.Payload
        }).ToList();
    }
}

public class EventDto
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public DateTime Timestamp { get; set; }
    public string Payload { get; set; }
}