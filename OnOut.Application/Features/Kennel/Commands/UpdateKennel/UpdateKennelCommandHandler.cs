using AutoMapper;
using FluentValidation;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using OnOut.Application.Exceptions;
using OnOut.Application.Features.Kennel.Commands.UpdateKennel;
using System.Threading;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Kennel.Commands.UpdateKennel
{
    public class UpdateKennelCommandHandler : IRequestHandler<UpdateKennelCommand, Unit>
    {
        private readonly IAppLogger<UpdateKennelCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateKennelCommand> _validator;
        private readonly IKennelRepository _kennelRepository;

        public UpdateKennelCommandHandler(
            IAppLogger<UpdateKennelCommandHandler> logger,
            IMapper mapper,
            IValidator<UpdateKennelCommand> validator,
            IKennelRepository kennelRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
            _kennelRepository = kennelRepository;
        }

        public async Task<Unit> Handle(UpdateKennelCommand request, CancellationToken cancellationToken)
        {
            // Validate the command
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for UpdateKennelCommand");
                throw new ValidationException(validationResult.Errors);
            }

            // Fetch the existing kennel entity
            var kennelEntity = await _kennelRepository.GetByIdAsync(request.Id);
            if (kennelEntity == null)
            {
                _logger.LogWarning($"Kennel with ID {request.Id} not found.");
                throw new NotFound($"Kennel with ID {request.Id} not found.", nameof(Kennel));
            }

            // Map the updated fields
            _mapper.Map(request, kennelEntity);

            // Update the kennel in the repository
            await _kennelRepository.UpdateAsync(kennelEntity);

            _logger.LogInformation($"Kennel with ID {request.Id} successfully updated.");

            return Unit.Value;
        }
    }
}