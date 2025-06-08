using MediatR;
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
        private readonly IMismanagmentHasherRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreateMismanagmentHasherCommandHandler> _logger;

        public CreateMismanagmentHasherCommandHandler(
            IValidator<CreateMismanagmentHasherCommand> validator,
            IMismanagmentHasherRepository repository,
            IMapper mapper,
            IAppLogger<CreateMismanagmentHasherCommandHandler> logger)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public Task<Guid> Handle(CreateMismanagmentHasherCommand request, CancellationToken cancellationToken)
        {
            var validationErrors = await _validator.ValidateAsync(request, cancellationToken);
            if(validationErrors.Any()){
                _logger.LogError("Validation errors occurred: {Errors}", validationErrors);
                throw new ValidationException(validationErrors);
            }

            var mismanagmentHasher = _mapper.Map<MisManagmentHashers>(request);
            await _repository.AddAsync(mismanagmentHasher, cancellationToken);
            _logger.LogInformation("MismanagmentHasher created with Id {Id}", mismanagmentHasher.Id);
            return mismanagmentHasher.Id;
        }
}
