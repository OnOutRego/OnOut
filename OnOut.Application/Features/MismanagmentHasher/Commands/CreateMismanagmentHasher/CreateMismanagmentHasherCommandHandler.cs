using AutoMapper;
using FluentValidation;
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
        public Task<Guid> Handle(CreateMismanagmentHasherCommand request, CancellationToken cancellationToken)
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
