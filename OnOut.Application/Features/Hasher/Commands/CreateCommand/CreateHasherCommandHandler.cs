﻿using AutoMapper;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using OnOut.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Hasher.Commands.CreateCommand
{
    public class CreateHasherCommandHandler : IRequestHandler<CreateHasherCommand, Guid>
    {
        private readonly IAppLogger<CreateHasherCommandHandler> _logger;
        private readonly IHasherRepository _hasherRepository;
        private readonly IMapper _mapper;

        public CreateHasherCommandHandler(IAppLogger<CreateHasherCommandHandler> logger, IHasherRepository hasherRepository, IMapper mapper)
        {
            this._logger = logger;
            this._hasherRepository = hasherRepository;
            this._mapper = mapper;
        }

        public async Task<Guid> Handle(CreateHasherCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateHasherCommandValidator(_hasherRepository);
            var validationResults = await validator.ValidateAsync(request, cancellationToken);
            if(validationResults.Errors.Any())
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

            var hasher = _mapper.Map<Domain.Hasher>(request);
            await _hasherRepository.CreateAsync(hasher);
            return hasher.Id;

        }
    }
}
