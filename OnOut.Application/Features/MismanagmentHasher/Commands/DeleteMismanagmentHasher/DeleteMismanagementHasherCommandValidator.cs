using FluentValidation;
using OnOut.Application.Contracts;

namespace OnOut.Application.Features.MismanagmentHasher.Commands.DeleteMismanagmentHasher
{
    public class DeleteMismanagementHasherCommandValidator : AbstractValidator<DeleteMismanagementHasherCommand>
    {
        private readonly IMismanagmentHashersRepository _repository;

        public DeleteMismanagementHasherCommandValidator(IMismanagmentHashersRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Id)
                .MustAsync(MismanExists)
                .WithMessage("MismanagmentHasher with the given ID does not exist.");
        }

        private async Task<bool> MismanExists(Guid guid, CancellationToken token)
        {
            return await _repository.ExistsById(guid);
        }
    }
}