using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;

namespace OnOut.Application.Features.MismanagmentHasher.Queries.GetByHasher
{

    public class GetByHasherQueryHandler : IRequestHandler<GetByHasherQuery, List<MismanagementHasherDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetByHasherQueryHandler> _logger;
        private readonly IMismanagmentHashersRepository _repository;

        public GetByHasherQueryHandler(
            IMapper mapper,
            IAppLogger<GetByHasherQueryHandler> logger,
            IMismanagmentHashersRepository repository)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        public async Task<List<MismanagementHasherDto>> Handle(GetByHasherQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching mismanagement hashers for hasher: {request.HasherId}");

            var entities = await _repository.GetByHasher(request.HasherId);
            if (entities == null || entities.Count == 0)
            {
                _logger.LogInformation($"No mismanagement hashers found for hasher: {request.HasherId}");
                return new List<MismanagementHasherDto>();
            }
            var dtos = _mapper.Map<List<MismanagementHasherDto>>(entities);

            return dtos;
        }
    }
}