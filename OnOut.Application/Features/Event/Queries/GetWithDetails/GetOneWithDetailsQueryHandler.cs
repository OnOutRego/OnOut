using MediatR;
using AutoMapper;
using OnOut.Application.Contracts.Logging;
using OnOut.Application.Contracts;
using OnOut.Application.Exceptions;

namespace OnOut.Application.Features.Event.Queries.GetWithDetails;
public class GetOneWithDetailsQueryHandler : IRequestHandler<GetOneWithDetailsQuery, EventDto>
{
    private readonly IAppLogger<GetOneWithDetailsQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IEventRepository _repository;

    public GetOneWithDetailsQueryHandler(IAppLogger<GetOneWithDetailsQueryHandler> logger, IMapper mapper, IEventRepository repository)
    {
        this._logger = logger;
        this._mapper = mapper;
        this._repository = repository;
    }
    public async Task<EventDto> Handle(GetOneWithDetailsQuery request, CancellationToken cancellationToken)
    {
        var specificEvent = await _repository.GetEventWithDetails(request.EventId);
        if (specificEvent == null)
        {
            _logger.LogWarning($"Could not find Event with Id {request.EventId}");
            throw new NotFound($"Could not find Event with Id: {request.EventId}", nameof(OnOut.Domain.Event));
        }
        var dto = _mapper.Map<EventDto>(specificEvent);
        return dto;
    }
}
