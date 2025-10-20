using AutoMapper;
using MediatR;
using OnOut.Application.Contracts.Logging;
using OnOut.Application.Contracts;
using OnOut.Domain;
using OnOut.Application.Exceptions;
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
        private readonly IEventRepository _repository;
        public GetAllEventQueryHandler(IAppLogger<GetAllEventQueryHandler> appLogger, IMapper mapper, IEventRepository repository)
        {
            this._logger = appLogger;
            this._mapper = mapper;
            this._repository = repository;
        }

        public async Task<List<EventListDto>> Handle(GetAllEventQuery request, CancellationToken cancellationToken)
        {
            var events = await _repository.GetAllAsync();
            if(events.Count < 0){
                throw new NotFound("Events returned null", nameof(Event));
            }

            var dto = _mapper.Map<List<EventListDto>>(events);
            return dto;
        }
    }
}
