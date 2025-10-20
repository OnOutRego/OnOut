using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;

namespace OnOut.Application.Features.KennelMember.Commands.DeleteKennelMember
{
    public class DeleteKennelMemberCommandHandler : IRequestHandler<DeleteKennelMemberCommand, Unit>
    {
        private readonly IValidator<DeleteKennelMemberCommand> _validator;
        private readonly IKennelMemberRepository _kennelMemberRepository;
        private readonly IAppLogger<DeleteKennelMemberCommandHandler> _logger;

        public DeleteKennelMemberCommandHandler(
            IValidator<DeleteKennelMemberCommand> validator,
            IKennelMemberRepository kennelMemberRepository,
            IAppLogger<DeleteKennelMemberCommandHandler> logger)
        {
            _validator = validator;
            _kennelMemberRepository = kennelMemberRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteKennelMemberCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for DeleteKennelMemberCommand: {Errors}", validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }
            var entity = await _kennelMemberRepository.GetByIdAsync(request.KennelMemberId);
            await _kennelMemberRepository.DeleteAsync(entity);
            _logger.LogInformation("Kennel member with ID {KennelMemberId} deleted.", request.KennelMemberId);

            return Unit.Value;
        }
    }
}