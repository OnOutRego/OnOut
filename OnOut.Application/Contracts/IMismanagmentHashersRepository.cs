using OnOut.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Contracts
{
    public interface IMismanagmentHashersRepository : IBaseRepository<MisManagmentHashers>
    {
        public Task<List<MisManagmentHashers>> GetByKennelId(Guid kennelId);
        public Task<List<MisManagmentHashers>> GetByHasher(Guid hasherId);
    }
}
