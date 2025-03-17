using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Hasher.Commands.DeleteCommand
{
    public class DeleteHasherCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
