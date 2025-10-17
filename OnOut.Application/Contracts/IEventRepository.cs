using OnOut.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Contracts
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        Task<Event> GetEventWithDetails(Guid eventId);
        Task<List<Event>> GetEventsForKennel(Guid kennelId);
        Task<List<Event>> GetEventsForHasher(Guid hasherId);
    }
}
