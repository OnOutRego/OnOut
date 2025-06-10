using System;
using MediatR;

namespace OnOut.Application.Features.MismanagmentHasher.Commands.DeleteMismanagmentHasher
{
    public class DeleteMismanagementHasherCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}