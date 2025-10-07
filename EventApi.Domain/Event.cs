namespace EventApi.Domain;

public class Event : Entity<Guid>
{
    public  string Type { get; private set; } // Ajout de 'required'
    public DateTime Timestamp { get; private set; }
    public string? Payload { get; private set; } // Nullable pour permettre null
    private Event() { } // Pour EF Core
    public Event(string type, string? payload)
    {
        Id = Guid.NewGuid();
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Timestamp = DateTime.UtcNow;
        Payload = payload;
    }
}