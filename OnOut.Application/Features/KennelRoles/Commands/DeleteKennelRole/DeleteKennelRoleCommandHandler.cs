using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnOut.Application.Interfaces;
using OnOut.Application.Common.Interfaces;
using OnOut.Application.Common.Logging;
using OnOut.Domain.Entities;

namespace OnOut.Application.Features.KennelRoles.Commands.DeleteKennelRole
{
    public class DeleteKennelRoleCommandHandler : IRequestHandler<DeleteKennelRoleCommand, Unit>
    {
        private readonly IKennelRolesRepository _kennelRolesRepository;
        private readonly IAppLogger<DeleteKennelRoleCommandHandler> _logger;

        public DeleteKennelRoleCommandHandler(
            IKennelRolesRepository kennelRolesRepository,
            IAppLogger<DeleteKennelRoleCommandHandler> logger)
        {
            _kennelRolesRepository = kennelRolesRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteKennelRoleCommand request, CancellationToken cancellationToken)
        {
            await _kennelRolesRepository.DeleteAsync(request.Id, cancellationToken);
            _logger.LogInformation($"KennelRole with Id {request.Id} deleted.");
            return Unit.Value;
        }
    }
}