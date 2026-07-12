using OnOut.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Contracts
{
    public interface IHasherRepository: IBaseRepository<Hasher>
    {
        Task<Hasher> GetDetailsAsync(Guid id);
        Task<bool> CheckExistsUserId(string userId);
        Task<Hasher?> GetByAuth0SubjectAsync(string auth0Subject);
    }
}
