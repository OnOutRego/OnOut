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

namespace OnOut.Application.Features.Hasher.Commands.UpdateCommand
{
    public class UpdateHasherCommandHandler : IRequestHandler<UpdateHasherCommand, Unit>
    {
        private readonly IAppLogger<UpdateHasherCommandHandler> _appLogger;
        private readonly IMapper _mapper;
        private readonly IHasherRepository _repository;
        private readonly IValidator<UpdateHasherCommand> _validator;

        public UpdateHasherCommandHandler(IAppLogger<UpdateHasherCommandHandler> appLogger, IMapper mapper, IHasherRepository repository, IValidator<UpdateHasherCommand> validator)
        {
            this._appLogger = appLogger;
            this._mapper = mapper;
            this._repository = repository;
            this._validator = validator;
        }
        public async Task<Unit> Handle(UpdateHasherCommand request, CancellationToken cancellationToken)
        {
            var validationResults = await _validator.ValidateAsync(request, cancellationToken);
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
