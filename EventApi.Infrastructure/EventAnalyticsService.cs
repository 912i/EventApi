using ClickHouse.Client.ADO;
using ClickHouse.Client.Utility;
using EventApi.Domain;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EventApi.Infrastructure;

public class EventAnalyticsService
{
    private readonly ClickHouseConnection _client;

    public EventAnalyticsService(IConfiguration config)
    {
        _client = new ClickHouseConnection(config.GetConnectionString("ClickHouse"));
    }

    public async Task InsertEventAsync(Event evt)
    {
        var cmd = _client.CreateCommand(); // Création sans paramètre
        cmd.CommandText = "INSERT INTO events (id, type, timestamp, payload) VALUES (@id, @type, @timestamp, @payload)";
        cmd.AddParameter("id", evt.Id.ToString());
        cmd.AddParameter("type", evt.Type);
        cmd.AddParameter("timestamp", evt.Timestamp);
        cmd.AddParameter("payload", evt.Payload ?? ""); // Gérer null comme chaîne vide
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<int> GetEventCountByTypeAsync(string type)
    {
        var cmd = _client.CreateCommand(); // Création sans paramètre
        cmd.CommandText = "SELECT COUNT(*) FROM events WHERE type = @type";
        cmd.AddParameter("type", type);
        return (int)await cmd.ExecuteScalarAsync();
    }
}