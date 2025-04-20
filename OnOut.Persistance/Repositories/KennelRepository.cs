using Microsoft.EntityFrameworkCore;
using OnOut.Application.Contracts;
using OnOut.Application.Features.Kennel;
using OnOut.Domain;
using OnOut.Persistance.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Persistance.Repositories
{
    public class KennelRepository : BaseRepository<Kennel>, IKennelRepository
    {
        private readonly OnOutDbContext _context;

        public KennelRepository(OnOutDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<Kennel>> GetKennelList()
        {
            var kennels = await _context.Kennels
                .Include(q=>q.Events)
                .Include(q=>q.Members)
                .ToListAsync();
            return kennels;
        }

        public async Task<Kennel> GetKennelWithDetails(Guid id)
        {
            var kennel = await _context.Kennels
                .Include(q => q.Events)
                .Include(q => q.Roles)
                .Include(q => q.Members)
                .ThenInclude(x => x.Hasher)
                .Include(q => q.Mismanagement)
                .ThenInclude(x => x.Hasher)
                .FirstOrDefaultAsync(x => x.Id == id);
            return kennel;
        }
    }
}
