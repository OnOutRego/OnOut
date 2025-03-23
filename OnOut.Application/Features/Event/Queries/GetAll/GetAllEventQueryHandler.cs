using AutoMapper;
using MediatR;
using OnOut.Application.Contracts.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Event.Queries.GetAll
{
    public class GetAllEventQueryHandler : IRequestHandler<GetAllEventQuery, List<EventListDto>>
    {
        private readonly IAppLogger<GetAllEventQueryHandler> _logger;
        private readonly IMapper _mapper;
        public GetAllEventQueryHandler(IAppLogger<GetAllEventQueryHandler> appLogger, IMapper mapper)
        {
            this._logger = appLogger;
            this._mapper = mapper;
        }

        public Task<List<EventListDto>> Handle(GetAllEventQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
