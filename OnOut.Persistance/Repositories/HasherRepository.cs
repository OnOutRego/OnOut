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
    public class HasherRepository : BaseRepository<Hasher>, IHasherRepository
    {
        private readonly OnOutDbContext _context;

        public HasherRepository(OnOutDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<bool> CheckExistsUserId(string userId)
        {
            return await _context.Hashers.AnyAsync(x => x.UserId == userId);
        }

        public async Task<Hasher> GetDetailsAsync(Guid id)
        {
            var hasher = await _context.Hashers
                .Include(q=> q.Diet)
                .FirstOrDefaultAsync(x => x.Id == id);
            return hasher;
        }
    }
}
