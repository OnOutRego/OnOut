using OnOut.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Contracts
{
    public interface IKennelMemberRepository: IBaseRepository<KennelMember>
    {
        public Task<List<KennelMember>> GetRolesByUser(Guid userId);
        
        public Task<List<KennelMember>> GetKennelMembersByKennelId(Guid kennelId);
    }
}
