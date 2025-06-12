using System;
using MediatR;

namespace OnOut.Application.Features.KennelMember.Commands.CreateKennelMember
{
    public class CreateKennelMemberCommand : IRequest<Guid>
    {
        public Guid HasherId { get; set; }
        public Guid KennelId { get; set; }
        public Guid RoleId { get; set; }
    }
}