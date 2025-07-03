using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using OnOut.Domain;

namespace OnOut.Application.Features.KennelMember.Commands.CreateKennelMember
{
    public class CreateKennelMemberCommandHandler : IRequestHandler<CreateKennelMemberCommand, Guid>
    {
        private readonly IValidator<CreateKennelMemberCommand> _validator;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreateKennelMemberCommandHandler> _logger;
        private readonly IKennelMemberRepository _kennelMemberRepository;

        public CreateKennelMemberCommandHandler(
            IValidator<CreateKennelMemberCommand> validator,
            IMapper mapper,
            IAppLogger<CreateKennelMemberCommandHandler> logger,
            IKennelMemberRepository kennelMemberRepository)
        {
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
            _kennelMemberRepository = kennelMemberRepository;
        }

        public async Task<Guid> Handle(CreateKennelMemberCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for CreateKennelMemberCommand: {Errors}", validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }
            
            var kennelMember = _mapper.Map<OnOut.Domain.KennelMember>(request);
            kennelMember.Id = Guid.NewGuid();

            await _kennelMemberRepository.CreateAsync(kennelMember);

            _logger.LogInformation("Kennel member created with Id: {Id}", kennelMember.Id);

            return kennelMember.Id;
        }
    }
}