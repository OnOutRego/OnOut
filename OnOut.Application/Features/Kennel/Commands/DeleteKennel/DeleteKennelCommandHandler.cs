using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;

namespace OnOut.Application.Features.Kennel.Commands.DeleteKennel
{
    public class DeleteKennelCommandHandler : IRequestHandler<DeleteKennelCommand, Unit>
    {
        private readonly IKennelRepository _kennelRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<DeleteKennelCommand> _validator;
        private readonly IAppLogger<DeleteKennelCommandHandler> _logger;

        public DeleteKennelCommandHandler(
            IKennelRepository kennelRepository,
            IMapper mapper,
            IValidator<DeleteKennelCommand> validator,
            IAppLogger<DeleteKennelCommandHandler> logger)
        {
            _kennelRepository = kennelRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteKennelCommand request, CancellationToken cancellationToken)
        {
            // Validate the command
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for DeleteKennelCommand: {Errors}", validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }

            // Check if the kennel exists
            var kennel = await _kennelRepository.GetByIdAsync(request.Id);
            if (kennel == null)
            {
                _logger.LogWarning("Kennel with Id {Id} not found.", request.Id);
                throw new KeyNotFoundException($"Kennel with Id {request.Id} not found.");
            }

            //Delete the KennelMembers associated with the kennel

            //Delete the misManagementList associated with the kennel

            //Delete the kennelRoles associated with the kennel

            // Delete the kennel
            await _kennelRepository.DeleteAsync(kennel);
            _logger.LogInformation("Kennel with Id {Id} was successfully deleted.", request.Id);

            return Unit.Value;
        }
    }
}
