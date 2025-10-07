using Microsoft.EntityFrameworkCore;
using EventApi.Domain;

namespace EventApi.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public AppDbContext(DbContextOptions options) : base(options) { }
}

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;
    public EventRepository(AppDbContext context) => _context = context;

    public async Task AddAsync(Event evt, CancellationToken ct)
    {
        await _context.Events.AddAsync(evt, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<List<Event>> GetByTypeAsync(string type, CancellationToken ct)
        => await _context.Events.Where(e => e.Type == type).ToListAsync(ct);
}