using MediatR;
using OnOut.Application.Features.Hasher.Queries.GetWithDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Hasher.Commands.UpdateCommand
{
    public class UpdateHasherCommand: IRequest<Unit>
    {
        public HasherDetailsDto Hasher {  get; set; }
        public string SenderId { get; set; }
    }
}
