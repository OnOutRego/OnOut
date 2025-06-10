using AutoMapper;
using FluentValidation;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using OnOut.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.MismanagmentHasher.Commands.CreateMismanagmentHasher
{
    public class CreateMismanagmentHasherCommandHandler : IRequestHandler<CreateMismanagmentHasherCommand, Guid>
    {
        private readonly IValidator<CreateMismanagmentHasherCommand> _validator;
        private readonly IMismanagmentHashersRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreateMismanagmentHasherCommandHandler> _logger;

        public CreateMismanagmentHasherCommandHandler(
            IValidator<CreateMismanagmentHasherCommand> validator,
            IMismanagmentHashersRepository repository,
            IMapper mapper,
            IAppLogger<CreateMismanagmentHasherCommandHandler> logger)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Guid> Handle(CreateMismanagmentHasherCommand request, CancellationToken cancellationToken)
        {
            var validationErrors = await _validator.ValidateAsync(request, cancellationToken);
            if (validationErrors.Errors.Any())
            {
                _logger.LogWarning("Validation errors occurred: {Errors}", validationErrors);
                throw new ValidationException(validationErrors.Errors);
            }

            var mismanagmentHasher = _mapper.Map<MisManagmentHashers>(request);
            await _repository.CreateAsync(mismanagmentHasher);
            _logger.LogInformation("MismanagmentHasher created with Id {Id}", mismanagmentHasher.Id);
            return mismanagmentHasher.Id;
        }
    }
}
