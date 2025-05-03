using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.KennelRoles.Commands.CreateKennelRole
{
    public class CreateKennelRoleCommandHandler : IRequestHandler<CreateKennelRoleCommand, Guid>
    {
        public Task<Guid> Handle(CreateKennelRoleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
