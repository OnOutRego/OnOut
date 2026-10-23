using OnOut.Application.Contracts;
using OnOut.Domain;
using OnOut.Persistance.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Persistance.Repositories
{
    public class EventTypeRepository : BaseRepository<EventType>, IEventTypeRepository
    {
        public EventTypeRepository(OnOutDbContext context) : base(context)
        {
            _context = context;
        }

        private readonly OnOutDbContext _context;

        public async Task<EventType?> GetByNameAsync(string name)
        {
            return await _context.EventTypes.FirstOrDefaultAsync(eventType => eventType.Name == name);
        }
    }
}
