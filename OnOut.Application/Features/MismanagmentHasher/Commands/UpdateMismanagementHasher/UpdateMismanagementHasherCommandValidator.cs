using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using OnOut.Application.Contracts;

namespace OnOut.Application.Features.MismanagmentHasher.Commands.UpdateMismanagementHasher
{
    public class UpdateMismanagementHasherCommandValidator : AbstractValidator<UpdateMismanagementHasherCommand>
    {
        private readonly IMismanagmentHashersRepository _repository;
        public UpdateMismanagementHasherCommandValidator(IMismanagmentHashersRepository repository)
        {
            _repository = repository;
            RuleFor(x => x.MismanagmentHasherId).NotEmpty().WithMessage("MismanagmentHasherId is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Position is required.");
            RuleFor(x => x.MismanagmentHasherId).MustAsync(MisManExists).WithMessage("MismanagmentHasher with the given ID does not exist.");
        }

        private async Task<bool> MisManExists(Guid guid, CancellationToken token)
        {
            return await _repository.ExistsById(guid);
        }
    }
   


}