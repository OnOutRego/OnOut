using FluentValidation;
using OnOut.Application.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace OnOut.Application.Features.KennelMember.Commands.UpdateKennelMember
{
    public class UpdateKennelMemberCommandValidator : AbstractValidator<UpdateKennelMemberCommand>
    {
        private readonly IKennelMemberRepository kennelMemberRepository;

        public UpdateKennelMemberCommandValidator(IKennelMemberRepository kennelMemberRepository)
        {
            RuleFor(x => x.KennelMemberId)
                .MustAsync(kennelMemberExists)
                .WithMessage("Kennel member does not exist.");
            this.kennelMemberRepository = kennelMemberRepository;
        }

        private async Task<bool> kennelMemberExists(Guid guid, CancellationToken token)
        {
            return await kennelMemberRepository.ExistsById(guid);
        }
    }

}