using MassTransit;
using EventApi.Domain;
using System.Threading.Tasks;

namespace EventApi.Application
{
    public interface IEventAnalyticsService
    {
        Task InsertEventAsync(Domain.Event evt);
        Task<int> GetEventCountByTypeAsync(string type);
    }
}
