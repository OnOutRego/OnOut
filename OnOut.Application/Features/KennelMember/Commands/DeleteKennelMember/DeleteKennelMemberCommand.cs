using MediatR;
using System;

namespace OnOut.Application.Features.KennelMember.Commands.DeleteKennelMember
{
    public class DeleteKennelMemberCommand : IRequest<Unit>
    {
        public Guid KennelMemberId { get; set; }
    }
}