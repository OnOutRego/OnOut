using OnOut.Application.Features.Kennel;
using OnOut.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Contracts
{
    public interface IKennelRepository : IBaseRepository<Kennel>
    {
        public Task<Kennel> GetKennelWithDetails(Guid id);
        public Task<List<Kennel>> GetKennelList();
    }
}
