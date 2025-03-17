using FluentValidation;
using OnOut.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Hasher.Commands.DeleteCommand
{
    public class DeleteHasherCommandValidator: AbstractValidator<DeleteHasherCommand>
    {
        private readonly IHasherRepository _hasherRepository;

        public DeleteHasherCommandValidator(IHasherRepository hasherRepository)
        {
            this._hasherRepository = hasherRepository;

            RuleFor(q => q)
                .MustAsync(HasherExists)
                .WithMessage("Hasher Does Not Exist!");
        }

        private async Task<bool> HasherExists(DeleteHasherCommand command, CancellationToken token)
        {
            return await _hasherRepository.ExistsById(command.Id);
        }
    }
}
