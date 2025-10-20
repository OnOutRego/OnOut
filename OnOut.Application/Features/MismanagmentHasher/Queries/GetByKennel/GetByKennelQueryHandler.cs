using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;

namespace OnOut.Application.Features.MismanagmentHasher.Queries.GetByKennel
{
 

    // Handler
    public class GetByKennelQueryHandler : IRequestHandler<GetByKennelQuery, List<MismanagementHasherDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetByKennelQueryHandler> _logger;
        private readonly IMismanagmentHashersRepository _repository;

        public GetByKennelQueryHandler(
            IMapper mapper,
            IAppLogger<GetByKennelQueryHandler> logger,
            IMismanagmentHashersRepository repository)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        public async Task<List<MismanagementHasherDto>> Handle(GetByKennelQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching mismanagement hashers for KennelId: {request.KennelId}");

            var hashers = await _repository.GetByKennelId(request.KennelId);
            if (hashers == null || hashers.Count == 0)
            {
                _logger.LogInformation($"No mismanagement hashers found for KennelId: {request.KennelId}");
                return new List<MismanagementHasherDto>();
            }
            var dtos = _mapper.Map<List<MismanagementHasherDto>>(hashers);

            return dtos;
        }
    }
}