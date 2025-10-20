using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.KennelRoles.Commands.CreateKennelRole
{
    public class CreateKennelRoleCommand: IRequest<Guid>
    {
        public Guid KennelId { get; set; }
        public string RoleName { get; set; }
    }
}
