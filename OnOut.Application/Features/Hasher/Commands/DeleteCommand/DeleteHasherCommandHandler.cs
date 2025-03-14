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

namespace OnOut.Application.Features.Hasher.Commands.DeleteCommand
{
    public class DeleteHasherCommandHandler : IRequestHandler<DeleteHasherCommand, Unit>
    {
        private readonly IAppLogger<DeleteHasherCommandHandler> _appLogger;
        private readonly IHasherRepository _hasherRepository;

        public DeleteHasherCommandHandler(IAppLogger<DeleteHasherCommandHandler> appLogger, IHasherRepository hasherRepository)
        {
            this._appLogger = appLogger;
            this._hasherRepository = hasherRepository;
        }
        public async Task<Unit> Handle(DeleteHasherCommand request, CancellationToken cancellationToken)
        {
           
           var validator = new DeleteHasherCommandValidator(_hasherRepository);
           var validationResults = await validator.ValidateAsync(request, cancellationToken);
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
