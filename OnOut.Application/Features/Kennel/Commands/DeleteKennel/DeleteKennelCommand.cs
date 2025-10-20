using System;
using MediatR;

namespace OnOut.Application.Features.Kennel.Commands.DeleteKennel
{
    public class DeleteKennelCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}