using FluentValidation;
using OnOut.Application.Contracts;

namespace OnOut.Application.Features.KennelMember.Commands.CreateKennelMember
{
    public class CreateKennelMemberCommandValidator : AbstractValidator<CreateKennelMemberCommand>
    {
        private readonly IHasherRepository hasherRepository;
        private readonly IKennelRepository kennelRepository;
        private readonly IKennelRolesRepository roleRepository;

        public CreateKennelMemberCommandValidator(IHasherRepository hasherRepository,
                                                  IKennelRepository kennelRepository,
                                                  IKennelRolesRepository roleRepository)
        {
            RuleFor(x => x.HasherId)
                .MustAsync(hasherExists)
                .WithMessage("Hasher does not exist.");
            RuleFor(x => x.KennelId)
                .MustAsync(kennelExists)
                .WithMessage("Kennel does not exist.");
            RuleFor(x => x.RoleId)
                .MustAsync(roleExists)
                .WithMessage("Role does not exist.");
            // Add more rules as needed based on CreateKennelMemberCommand properties


            this.hasherRepository = hasherRepository;
            this.kennelRepository = kennelRepository;
            this.roleRepository = roleRepository;
        }

        private async Task<bool> roleExists(Guid guid, CancellationToken token)
        {
            return await roleRepository.ExistsById(guid);
        }

        private async Task<bool> kennelExists(Guid guid, CancellationToken token)
        {
            return await kennelRepository.ExistsById(guid);
        }

        private async Task<bool> hasherExists(Guid guid, CancellationToken token)
        {
            return await hasherRepository.ExistsById(guid);
        }
    }
}