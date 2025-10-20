using AutoMapper;
using FluentValidation;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using OnOut.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.KennelRoles.Commands.CreateKennelRole
{
    public class CreateKennelRoleCommandHandler : IRequestHandler<CreateKennelRoleCommand, Guid>
    {
        private readonly IAppLogger<CreateKennelRoleCommandHandler> _logger;
        private readonly IKennelRolesRepository _kennelRolesRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateKennelRoleCommand> _validator;

        public CreateKennelRoleCommandHandler(IAppLogger<CreateKennelRoleCommandHandler> logger, IKennelRolesRepository kennelRolesRepository, IMapper mapper, IValidator<CreateKennelRoleCommand> validator)
        {
            _logger = logger;
            _kennelRolesRepository = kennelRolesRepository;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateKennelRoleCommand request, CancellationToken cancellationToken)
        {
            var validationResults = await _validator.ValidateAsync(request, cancellationToken);
            if (validationResults.Errors.Any())
            {
                _logger.LogWarning("Validation failed for command {CommandName} with errors: {Errors}", nameof(CreateKennelRoleCommand), validationResults.Errors);
                throw new BadRequest(validationResults.Errors.First().ErrorMessage);
            }

            var kennelRole = _mapper.Map<Domain.KennelRoles>(request);
            await _kennelRolesRepository.CreateAsync(kennelRole);
            return kennelRole.Id;
        }
    }
}
