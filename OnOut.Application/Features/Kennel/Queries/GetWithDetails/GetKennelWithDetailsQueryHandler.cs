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

namespace OnOut.Application.Features.Kennel.Queries.GetWithDetails
{
    public class GetKennelWithDetailsQueryHandler : IRequestHandler<GetKennelWithDetailsQuery, KennelDto>
    {
        private readonly IAppLogger<GetKennelWithDetailsQueryHandler> _appLogger;
        private readonly IKennelRepository _repository;
        private readonly IMapper _mapper;

        public GetKennelWithDetailsQueryHandler(IAppLogger<GetKennelWithDetailsQueryHandler> appLogger, IKennelRepository repository, IMapper mapper)
        {
            this._appLogger = appLogger;
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<KennelDto> Handle(GetKennelWithDetailsQuery request, CancellationToken cancellationToken)
        {
            var kennel = await _repository.GetKennelWithDetails(request.Id);
            if (kennel == null)
            {
                throw new NotFound($"Kennel of Id {request.Id} not found!", nameof(Kennel));
            }
            var dto = _mapper.Map<KennelDto>(kennel);
            return dto;
        }
    }
}
