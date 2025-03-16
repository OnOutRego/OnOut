using AutoMapper;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Hasher.Commands.CreateCommand
{
    public class CreateHasherCommandHandler : IRequestHandler<CreateHasherCommand, Guid>
    {
        public CreateHasherCommandHandler()
        {

        }

        public Task<Guid> Handle(CreateHasherCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
