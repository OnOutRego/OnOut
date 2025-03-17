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
            RuleFor(q => q.Hasher.Id)
                .MustAsync(PermissionsExist)
                .WithMessage("You do not have permission to Edit this Hasher");
            
        }

        private async Task<bool> PermissionsExist(Guid guid, CancellationToken token)
        {
            //TODO: Authentication will be required to check claims IF the editor is NOT the hasher
            return true;
        }

        private async Task<bool> HasherExists(UpdateHasherCommand command, CancellationToken token)
        {
            return await _hasherRepository.ExistsById(command.Hasher.Id);
        }
    }
}
