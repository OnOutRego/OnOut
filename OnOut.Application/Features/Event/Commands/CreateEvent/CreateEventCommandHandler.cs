using FluentValidation;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using OnOut.Application.Exceptions;
using OnOut.Domain;
namespace OnOut.Application.Features.Event.Commands.CreateEvent;
public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IAppLogger<CreateEventCommandHandler> _logger;
    private readonly IEventRepository _repository;
    private readonly IEventTypeRepository _eventTypeRepository;
    private readonly IValidator<CreateEventCommand> _validator;

    public CreateEventCommandHandler(IAppLogger<CreateEventCommandHandler> logger, IEventRepository repository, IEventTypeRepository eventTypeRepository, IValidator<CreateEventCommand> validator)
    {
        this._logger = logger;
        this._repository = repository;
        this._eventTypeRepository = eventTypeRepository;
        this._validator = validator;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var validationResults = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResults.Errors.Any())
        {
            _logger.LogWarning("Errors have occured");
            foreach (var validationResult in validationResults.Errors)
            {
                _logger.LogWarning(validationResult.ErrorMessage);
                if (validationResult.ErrorMessage.Contains("Exists"))
                {
                    throw new BadRequest(validationResult.ErrorMessage);
                }
            }

            throw new BadRequest("Request Must Contain a UserId");
        }

        var eventTypeName = request.EventTypeName.Trim();
        var eventType = await _eventTypeRepository.GetByNameAsync(eventTypeName);
        if (eventType == null)
        {
            eventType = new EventType { Id = Guid.NewGuid(), Name = eventTypeName };
            await _eventTypeRepository.CreateAsync(eventType);
        }

        var newEvent = new OnOut.Domain.Event
        {
            Id = Guid.NewGuid(),
            CreatorId = request.CreatorId,
            EventTypeId = eventType.Id,
            Name = request.Name.Trim(),
            Description = request.Description,
            Location = new Address { Id = Guid.NewGuid(), Name = request.LocationName.Trim() },
            TrailTime = request.TrailTime,
            ShiggyLevel = request.ShiggyLevel,
            HasKennel = request.KennelId.HasValue,
            KennelId = request.KennelId,
            MaxRegos = request.MaxRegos,
            HasGimmes = request.HasGimmes,
            Gimmes = request.Gimmes,
            HasEarlyRego = request.HasEarlyRego,
            EarlyRegoDeadline = request.EarlyRegoDeadline,
            EarlyRegoPrice = request.EarlyRegoPrice,
            RegoPrice = request.RegoPrice,
            RegoDeadline = request.RegoDeadline,
            PrimaryImage = request.PrimaryImage,
            BannerImage = request.BannerImage
        };

        await _repository.CreateAsync(newEvent);
        return newEvent.Id;
    }
}
