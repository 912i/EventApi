using MassTransit;
using EventApi.Domain;

namespace EventApi.Infrastructure;

public class EventMessage
{
        public string Type { get; set; }
        public string Payload { get; set; }
}
