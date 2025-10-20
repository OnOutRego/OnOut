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
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(OnOutDbContext context) : base(context)
        {
        }
    }
}
