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

namespace OnOut.Application.Features.Hasher.Queries.GetWithDetails
{
    public class GetHasherDetailsQueryHandler : IRequestHandler<GetHasherDetailsQuery, HasherDetailsDto>
    {
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetHasherDetailsQueryHandler> _appLogger;
        private readonly IHasherRepository _repository;

        public GetHasherDetailsQueryHandler(IMapper mapper, IAppLogger<GetHasherDetailsQueryHandler> appLogger, IHasherRepository repository)
        {
            this._mapper = mapper;
            this._appLogger = appLogger;
            this._repository = repository;
        }
        public async Task<HasherDetailsDto> Handle(GetHasherDetailsQuery request, CancellationToken cancellationToken)
        {
            var hasher = _repository.GetDetailsAsync(request.Id);
            if (hasher == null)
            {
                _appLogger.LogWarning($"Could not find Hasher with Id {request.Id}");
                throw new NotFound($"Hasher could not be found", nameof(request.Id));
            }

            var dto = _mapper.Map<HasherDetailsDto>(hasher);
            return dto;
        }
    }
}
