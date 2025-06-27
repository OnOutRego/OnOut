using AutoMapper;
using FluentValidation;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.MismanagmentHasher.Commands.CreateMismanagmentHasher
{
    public class CreateMismanagmentHasherCommandHandler : IRequestHandler<CreateMismanagmentHasherCommand, Guid>
    {
        private readonly IAppLogger<CreateMismanagmentHasherCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMismanagmentHashersRepository _repository;
        private readonly IValidator<CreateMismanagmentHasherCommand> _validator;

        public CreateMismanagmentHasherCommandHandler(
            IAppLogger<CreateMismanagmentHasherCommandHandler> logger,
            IMapper mapper,
            IMismanagmentHashersRepository repository,
            IValidator<CreateMismanagmentHasherCommand> validator)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
        }
        public Task<Guid> Handle(CreateMismanagmentHasherCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
