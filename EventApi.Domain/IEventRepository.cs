namespace EventApi.Domain;

public interface IEventRepository
{
    Task AddAsync(Event evt, CancellationToken ct);
    Task<List<Event>> GetByTypeAsync(string type, CancellationToken ct);
}