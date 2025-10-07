using Microsoft.Extensions.Caching.Distributed;
using EventApi.Domain;

namespace EventApi.Infrastructure;

public class EventCountService
{
    private readonly IDistributedCache _cache;
    private readonly AppDbContext _context;

    public EventCountService(IDistributedCache cache, AppDbContext context)
    {
        _cache = cache;
        _context = context;
    }

    public async Task<int> GetEventCountAsync(string type)
    {
        var key = $"count:{type}";
        var cached = await _cache.GetStringAsync(key);
        if (cached != null) return int.Parse(cached);

        var count = await _context.Events.CountAsync(e => e.Type == type);
        await _cache.SetStringAsync(key, count.ToString(), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        });
        return count;
    }
}