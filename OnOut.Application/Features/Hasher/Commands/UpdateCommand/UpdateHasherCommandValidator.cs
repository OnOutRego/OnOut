using FluentValidation;
using OnOut.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Hasher.Commands.UpdateCommand
{
    public class UpdateHasherCommandValidator:AbstractValidator<UpdateHasherCommand>
    {
        private readonly IHasherRepository _hasherRepository;

        public UpdateHasherCommandValidator(IHasherRepository hasherRepository) 
        {
            this._hasherRepository = hasherRepository;

            RuleFor(q => q)
                .MustAsync(HasherExists)
                .WithMessage("Hasher does not exist!");
            RuleFor(q => q)
                .MustAsync(PermissionsExist)
                .WithMessage("You do not have permission to Edit this Hasher");
            
        }

        private async Task<bool> PermissionsExist(UpdateHasherCommand command, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(command.SenderId))
            {
                return false;
            }

            var hasher = await _hasherRepository.GetByAuth0SubjectAsync(command.SenderId);
            return hasher?.Id == command.Hasher.Id;
        }

        private async Task<bool> HasherExists(UpdateHasherCommand command, CancellationToken token)
        {
            return await _hasherRepository.ExistsById(command.Hasher.Id);
        }
    }
}
