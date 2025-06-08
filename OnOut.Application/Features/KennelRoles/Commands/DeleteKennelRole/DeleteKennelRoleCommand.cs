using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.KennelRoles.Commands.DeleteKennelRole
{
    public class DeleteKennelRoleCommand: IRequest<Guid>
    {
        public Guid RoleId { get; set; }
    }
}
