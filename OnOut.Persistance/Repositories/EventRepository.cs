using Microsoft.EntityFrameworkCore;
using OnOut.Application.Contracts;
using OnOut.Domain;
using OnOut.Persistance.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Persistance.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        private readonly OnOutDbContext _context;

        public EventRepository(OnOutDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Event> GetEventWithDetails(Guid eventId)
        {
            return await _context.Events
                            .Include(q => q.Regos)
                            .ThenInclude(x => x.Hasher)
                            .Include(q => q.EventKennel)
                            .Include(q => q.Location)
                            .Include(q => q.EventType)
                            .FirstOrDefaultAsync(q => q.Id == eventId);
        }
        public async Task<List<Event>> GetEventsForKennel(Guid kennelId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Event>> GetEventsForHasher(Guid hasherId)
        {
            throw new NotImplementedException();
        }

    }
}
