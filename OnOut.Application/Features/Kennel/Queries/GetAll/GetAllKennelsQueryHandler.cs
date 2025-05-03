using AutoMapper;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Kennel.Queries.GetAll
{
    public class GetAllKennelsQueryHandler : IRequestHandler<GetAllKennelsQuery, List<KennelListDto>>
    {
        private readonly IAppLogger<GetAllKennelsQueryHandler> _appLogger;
        private readonly IMapper _mapper;
        private readonly IKennelRepository _repository;

        public GetAllKennelsQueryHandler(IAppLogger<GetAllKennelsQueryHandler> appLogger, IMapper mapper, IKennelRepository repository)
        {
            this._appLogger = appLogger;
            this._mapper = mapper;
            this._repository = repository;
        }
        public async Task<List<KennelListDto>> Handle(GetAllKennelsQuery request, CancellationToken cancellationToken)
        {
            var kennels = await _repository.GetAllAsync();
            if (kennels == null || !kennels.Any())
            {
                _appLogger.LogWarning("No kennels found.");
            }

            var kennelList = _mapper.Map<List<KennelListDto>>(kennels);
            _appLogger.LogInformation($"Retrieved {kennelList.Count} kennels.");
            return kennelList;
        }
    }
}
