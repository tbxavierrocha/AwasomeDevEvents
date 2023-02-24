using AwasomeDevEvents.Entities;

namespace AwasomeDevEvents.Persistence
{
    public class DevEventsDbContext
    {
        public List<DevEvent> DevEvents { get; set; }

        public DevEventsDbContext()
        {
            DevEvents = new List<DevEvent>();
        }
    }
}
