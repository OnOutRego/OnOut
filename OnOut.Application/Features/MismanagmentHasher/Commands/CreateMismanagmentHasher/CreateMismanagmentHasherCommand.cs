using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.MismanagmentHasher.Commands.CreateMismanagmentHasher
{
    public class CreateMismanagmentHasherCommand : IRequest<Guid>
    {
        public Guid HasherId { get; set; }
        public Guid KennelId { get; set; }
        public string Position { get; set; }
    }
}
