using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;

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
            var role = await _kennelRolesRepository.GetByIdAsync(request.RoleId);
            await _kennelRolesRepository.DeleteAsync(role);
            _logger.LogInformation($"KennelRole with Id {request.RoleId} deleted.");
            return Unit.Value;
        }
    }
}