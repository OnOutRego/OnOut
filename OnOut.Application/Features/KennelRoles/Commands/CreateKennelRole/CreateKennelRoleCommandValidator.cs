using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using OnOut.Application.Contracts;

namespace OnOut.Application.Features.KennelRoles.Commands.CreateKennelRole
{
    public class CreateKennelRoleCommandValidator: AbstractValidator<CreateKennelRoleCommand>
    {
        private readonly IKennelRolesRepository _kennelRolesRepository;

        public CreateKennelRoleCommandValidator(IKennelRolesRepository kennelRolesRepository)
        {
            this._kennelRolesRepository = kennelRolesRepository;

            RuleFor(x => x)
                .MustAsync(async (request, cancellation) => !await _kennelRolesRepository.ExistsByName(request.RoleName))
                .WithMessage("Role name already exists in this kennel.");
        }
    }
}
