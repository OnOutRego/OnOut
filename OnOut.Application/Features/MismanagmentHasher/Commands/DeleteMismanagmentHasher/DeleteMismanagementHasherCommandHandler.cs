using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;

namespace OnOut.Application.Features.MismanagmentHasher.Commands.DeleteMismanagmentHasher
{
    public class DeleteMismanagementHasherCommandHandler : IRequestHandler<DeleteMismanagementHasherCommand, Unit>
    {
        private readonly IMismanagmentHashersRepository _repository;
        private readonly IAppLogger<DeleteMismanagementHasherCommandHandler> _logger;
        private readonly IValidator<DeleteMismanagementHasherCommand> _validator;

        public DeleteMismanagementHasherCommandHandler(
            IMismanagmentHashersRepository repository,
            IAppLogger<DeleteMismanagementHasherCommandHandler> logger,
            IValidator<DeleteMismanagementHasherCommand> validator)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Unit> Handle(DeleteMismanagementHasherCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for DeleteMismanagementHasherCommand: {Errors}", validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }
            var misman = await _repository.GetByIdAsync(request.Id);
            if (misman == null)
            {
                _logger.LogWarning("MismanagementHasher with Id {Id} not found.", request.Id);
                throw new KeyNotFoundException($"MismanagementHasher with Id {request.Id} not found.");
            }
            await _repository.DeleteAsync(misman);
            _logger.LogInformation("Deleted MismanagementHasher with Id: {Id}", request.Id);

            return Unit.Value;
        }
    }
}