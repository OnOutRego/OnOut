using FluentValidation;
using AutoMapper;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using OnOut.Application.Exceptions;
namespace OnOut.Application.Features.Event.Commands.CreateEvent;
public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IAppLogger<CreateEventCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IEventRepository _repository;
    private readonly IValidator<CreateEventCommand> _validator;

    public CreateEventCommandHandler(IAppLogger<CreateEventCommandHandler> logger, IMapper mapper, IEventRepository repository, IValidator<CreateEventCommand> validator)
    {
        this._logger = logger;
        this._mapper = mapper;
        this._repository = repository;
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

        var newEvent = _mapper.Map<Domain.Event>(request);
        await _repository.CreateAsync(newEvent);
        return newEvent.Id;
    }
}
