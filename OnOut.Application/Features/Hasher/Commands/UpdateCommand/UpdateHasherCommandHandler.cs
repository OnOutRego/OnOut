using AutoMapper;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using OnOut.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Hasher.Commands.UpdateCommand
{
    public class UpdateHasherCommandHandler : IRequestHandler<UpdateHasherCommand, Unit>
    {
        private readonly IAppLogger<UpdateHasherCommandHandler> _appLogger;
        private readonly IMapper _mapper;
        private readonly IHasherRepository _repository;

        public UpdateHasherCommandHandler(IAppLogger<UpdateHasherCommandHandler> appLogger, IMapper mapper, IHasherRepository repository)
        {
            this._appLogger = appLogger;
            this._mapper = mapper;
            this._repository = repository;
        }
        public async Task<Unit> Handle(UpdateHasherCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateHasherCommandValidator(_repository);
            var validationResults = await validator.ValidateAsync(request, cancellationToken);
            if(validationResults.Errors.Any())
            {
                _appLogger.LogWarning("Errors Occured");
                foreach (var validationResult in validationResults.Errors)
                {
                    _appLogger.LogWarning(validationResult.ErrorMessage);
                    if (validationResult.ErrorMessage.Contains("Permission")){
                        throw new BadRequest(validationResult.ErrorMessage);
                    }
                }
                throw new NotFound($"Hasher Not found with ID: {request.Hasher.Id}", nameof(Hasher));
            }

            var hasher = _mapper.Map<Domain.Hasher>(request.Hasher);
            await _repository.UpdateAsync(hasher);
            return Unit.Value;
        }
    }
}
