 using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Threading.Tasks;
 using Microsoft.EntityFrameworkCore;
using OnOut.Application.Contracts;
using OnOut.Domain;
using OnOut.Persistance.DatabaseContext;
namespace OnOut.Persistance.Repositories
{
    public class KennelRolesRepository : BaseRepository<KennelRoles>,IKennelRolesRepository
    {
        private readonly OnOutDbContext _context;

        public KennelRolesRepository(OnOutDbContext context) : base(context)
        {
            _context = context;
        }
    }
}