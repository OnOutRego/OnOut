using FluentValidation;
using OnOut.Application.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace OnOut.Application.Features.KennelMember.Commands.DeleteKennelMember
{
    public class DeleteKennelMemberCommandValidator : AbstractValidator<DeleteKennelMemberCommand>
    {
        private readonly IKennelMemberRepository kennelMemberRepository;

        public DeleteKennelMemberCommandValidator(IKennelMemberRepository kennelMemberRepository)
        {
            this.kennelMemberRepository = kennelMemberRepository;
            RuleFor(x => x.KennelMemberId)
                .MustAsync(KennelMemberExists)
                .WithMessage("Kennel member does not exist.");
        }


        private async Task<bool> KennelMemberExists(Guid kennelMemberId, CancellationToken cancellationToken)
        {
            // TODO: Implement actual existence check, e.g. via repository or DbContext
            return await kennelMemberRepository.ExistsById(kennelMemberId);
        }
    }
}