using AutoMapper;
using FluentValidation;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using OnOut.Application.Exceptions;
using OnOut.Application.Features.KennelRoles.Commands.CreateKennelRole;
using OnOut.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Kennel.Commands.CreateKennel
{
    public class CreateKennelCommandHandler : IRequestHandler<CreateKennelCommand, Guid>
    {
        private readonly IValidator<CreateKennelCommand> _validator;
        private readonly IAppLogger<CreateKennelCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IKennelRepository _kennelRepository;
        private readonly IMediator _mediator;

        public CreateKennelCommandHandler(IValidator<CreateKennelCommand> validator, IAppLogger<CreateKennelCommandHandler> appLogger, IMapper mapper, IKennelRepository kennelRepository, IMediator mediator)
        {
            this._validator = validator;
            this._logger = appLogger;
            this._mapper = mapper;
            this._kennelRepository = kennelRepository;
            this._mediator = mediator;
        }
        public async Task<Guid> Handle(CreateKennelCommand request, CancellationToken cancellationToken)
        {
            //validate
            var validationResults = await _validator.ValidateAsync(request, cancellationToken);
            if (validationResults.Errors.Any())
            {
                _logger.LogWarning("Errors have occured");
                foreach (var validationResult in validationResults.Errors)
                {
                    _logger.LogWarning(validationResult.ErrorMessage);
                    if (validationResult.ErrorMessage.Contains("Exists"))
                    {
                        throw new BadRequest(validationResult.ErrorMessage);
                    }
                }

                throw new BadRequest("Request Must Contain a UserId");
            }
            //Create Kennel
            var kennel = _mapper.Map<Domain.Kennel>(request);
            await _kennelRepository.CreateAsync(kennel);
            //Create Roles
            var adminRoleId = await CreateKennelRoles(kennel.Id, kennel.Name) ;
            //TODO Create KennelMember
            await _mediator.Send(new CreateKennelMemberCommand() { UserId = request.FounderId, KennelId = kennel.Id, RoleId = adminRoleId });
            return kennel.Id;

        }

        private async Task<Guid> CreateKennelRoles(Guid kennelId, string kennelName)
        {
            List<string> newRoles = new List<string> { 
                $"{kennelName}_MisManagment",
                $"{kennelName}_Member",
            };
            //createAdminRole
            var adminId = await _mediator.Send(new CreateKennelRoleCommand() { KennelId = kennelId, RoleName = $"{kennelName}_Admin" });

            //createOtherRoles
            foreach (var role in newRoles)
            {
                await _mediator.Send(new CreateKennelRoleCommand() { KennelId = kennelId, RoleName = role });
            }

            return adminId;
        }
    }

    internal class CreateKennelMemberCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid KennelId { get; set; }
        public Guid RoleId { get; set; }
    }
}
