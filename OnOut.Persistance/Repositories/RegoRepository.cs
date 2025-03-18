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
    public class RegoRepository : BaseRepository<Rego>, IRegoRepository
    {
        public RegoRepository(OnOutDbContext context) : base(context)
        {
        }
    }
}
