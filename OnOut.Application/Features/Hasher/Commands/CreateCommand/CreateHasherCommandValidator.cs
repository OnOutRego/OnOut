using FluentValidation;
using OnOut.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Hasher.Commands.CreateCommand
{
    public class CreateHasherCommandValidator:AbstractValidator<CreateHasherCommand>
    {
        private readonly IHasherRepository _hasherRepository;

        public CreateHasherCommandValidator(IHasherRepository hasherRepository)
        {
            this._hasherRepository = hasherRepository;

            RuleFor(q => q.UserId)
                .NotNull().WithMessage("Must Have a UserId")
                .NotEmpty().WithMessage("Must Have a UserId")
                .MustAsync(HasherExists)
                .WithMessage("A Hasher Already Exists with this UserId");
            
        }

        private async Task<bool> HasherExists(string userId, CancellationToken token)
        {
                var exists = await _hasherRepository.CheckExistsUserId(userId);
                return !exists;
        }
    }
}
