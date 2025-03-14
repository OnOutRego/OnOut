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

namespace OnOut.Application.Features.Hasher.Queries.GetAll
{
    public class GetAllHasherCommandHandler : IRequestHandler<GetAllHasherQuery, List<HasherDto>>
    {
        private readonly GetAllHasherQuery _request;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetAllHasherCommandHandler> _logger;
        private readonly IHasherRepository _hasherRepository;

        public GetAllHasherCommandHandler(GetAllHasherQuery request, IMapper mapper, IAppLogger<GetAllHasherCommandHandler> logger, IHasherRepository hasherRepository)
        {
            this._request = request;
            this._mapper = mapper;
            this._logger = logger;
            this._hasherRepository = hasherRepository;
        }
        public async Task<List<HasherDto>> Handle(GetAllHasherQuery request, CancellationToken cancellationToken)
        {
            var hasher = await _hasherRepository.GetAllAsync();
            if(hasher.Count <= 0)
            {
                _logger.LogWarning("Hashers Not Found");
                throw new NotFound("Hashers returned null", nameof(hasher));
            }

            var dto = _mapper.Map<List<HasherDto>>(hasher);
            return dto;
        }
    }
}
