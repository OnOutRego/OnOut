using System;
using MediatR;

namespace OnOut.Application.Features.KennelMember.Commands.UpdateKennelMember
{
    public class UpdateKennelMemberCommand : IRequest<Unit>
    {
        public Guid KennelMemberId { get; set; }
        public Guid RoleId { get; set; }
    }
}