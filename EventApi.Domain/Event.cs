using System.ComponentModel.DataAnnotations;

namespace EventApi.Domain
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Timestamp { get; set; }
        public string Payload { get; set; }
    }
}