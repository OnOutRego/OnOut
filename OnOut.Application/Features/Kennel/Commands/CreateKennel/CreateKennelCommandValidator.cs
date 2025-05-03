using FluentValidation;
using OnOut.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Kennel.Commands.CreateKennel
{
    public class CreateKennelCommandValidator:AbstractValidator<CreateKennelCommand>
    {
        private readonly IKennelRepository _kennelRepository;
        private readonly IHasherRepository _hasherRepository;

        public CreateKennelCommandValidator(IKennelRepository kennelRepository, IHasherRepository hasherRepository)
        {
            this._kennelRepository = kennelRepository;
            this._hasherRepository = hasherRepository;
            RuleFor(x => x)
                .MustAsync(KennnelExists)
                .WithMessage("Kennel with this name already exists");
            RuleFor(x => x.FounderId)
                .MustAsync(UserExists)
                .WithMessage("User with this id does not exist");
        }

        private async Task<bool> KennnelExists(CreateKennelCommand command, CancellationToken token)
        {
            var exists = await _kennelRepository.ExistsByName(command.Name);
            return !exists;
        }

        private async Task<bool> UserExists(Guid userId, CancellationToken token)
        {
            var exists = await _hasherRepository.ExistsById(userId);
            return exists;
        }
    }
}
