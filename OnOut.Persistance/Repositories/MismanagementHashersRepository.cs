using Microsoft.EntityFrameworkCore;
using OnOut.Application.Contracts;
using OnOut.Domain;
using OnOut.Persistance.DatabaseContext;

namespace OnOut.Persistance.Repositories
{
    public class MismanagementHashersRepository : BaseRepository<MisManagmentHashers>, IMismanagmentHashersRepository
    {
        private readonly OnOutDbContext _context;

        public MismanagementHashersRepository(OnOutDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<MisManagmentHashers>> GetByKennelId(Guid kennelId)
        {
            return await _context.Mismanagement
                .Where(m => m.KennelId == kennelId)
                .ToListAsync();
        }

        public async Task<List<MisManagmentHashers>> GetByHasher(Guid hash)
        {
            return await _context.Mismanagement
                .Where(m => m.HasherId == hash)
                .ToListAsync();
        }
    }
}