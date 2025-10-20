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

namespace OnOut.Application.Features.Hasher.Commands.DeleteCommand
{
    public class DeleteHasherCommandHandler : IRequestHandler<DeleteHasherCommand, Unit>
    {
        private readonly IAppLogger<DeleteHasherCommandHandler> _appLogger;
        private readonly IHasherRepository _hasherRepository;
        private readonly IValidator<DeleteHasherCommand> _validator;

        public DeleteHasherCommandHandler(IAppLogger<DeleteHasherCommandHandler> appLogger, IHasherRepository hasherRepository, IValidator<DeleteHasherCommand> validator)
        {
            this._appLogger = appLogger;
            this._hasherRepository = hasherRepository;
            this._validator = validator;
        }
        public async Task<Unit> Handle(DeleteHasherCommand request, CancellationToken cancellationToken)
        {
           
           var validationResults = await _validator.ValidateAsync(request, cancellationToken);
            if (validationResults.Errors.Any()) 
            {
                _appLogger.LogWarning("Errors Occured");
                foreach( var validationResult in validationResults.Errors )
                {
                    _appLogger.LogWarning(validationResult.ErrorMessage);
                }

                throw new BadRequest($"Errors Occured trying to delete Hasher: {request.Id}");
            }

            var hasher = await _hasherRepository.GetByIdAsync(request.Id);
            await _hasherRepository.DeleteAsync(hasher);
            return Unit.Value;
        }
    }
}
