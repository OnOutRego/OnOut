using System;
using MediatR;

namespace OnOut.Application.Features.MismanagmentHasher.Commands.UpdateMismanagementHasher
{
    public class UpdateMismanagementHasherCommand : IRequest<Unit>
    {
        public Guid MismanagmentHasherId { get; set; }
        public string Name { get; set; }
    }
}