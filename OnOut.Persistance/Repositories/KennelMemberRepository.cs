using Microsoft.EntityFrameworkCore;
using OnOut.Application.Contracts;
    using OnOut.Domain;
    using OnOut.Persistance.DatabaseContext;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

namespace OnOut.Persistance.Repositories {

    public class KennelMemberRepository : BaseRepository<KennelMember>, IKennelMemberRepository
    {
        private readonly OnOutDbContext _context;

        public KennelMemberRepository(OnOutDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<KennelMember>> GetKennelMembersByKennelId(Guid kennelId)
        {
            var members = await _context.KennelMembers
                .Include(x => x.Hasher)
                .Include(x => x.Role)
                .Where(x => x.KennelId == kennelId)
                .ToListAsync();
            return members;
        }

        public async Task<List<KennelMember>> GetRolesByUser(Guid userId)
        {
            var roles = await _context.KennelMembers
                .Include(x => x.Role)
                .Where(x => x.HasherId == userId)
                .ToListAsync();
            return roles;
        }
    }
}