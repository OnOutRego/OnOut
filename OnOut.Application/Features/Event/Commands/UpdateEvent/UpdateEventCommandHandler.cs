using MediatR;
using AutoMapper;
using FluentValidation;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using OnOut.Application.Exceptions;

namespace OnOut.Application.Features.Event.Commands.UpdateEvent;
public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Unit>
{
    private readonly IAppLogger<UpdateEventCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IEventRepository _repository;
    private readonly IValidator<UpdateEventCommand> _validator;

    public UpdateEventCommandHandler(IAppLogger<UpdateEventCommandHandler> logger, IMapper mapper, IEventRepository repository, IValidator<UpdateEventCommand> validator)
    {
        this._logger = logger;
        this._mapper = mapper;
        this._repository = repository;
        this._validator = validator;
    }
    public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var validationResults = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResults.Errors.Any())
        {
            _logger.LogWarning("Errors Occured");
            foreach (var validationResult in validationResults.Errors)
            {
                _logger.LogWarning(validationResult.ErrorMessage);
                if (validationResult.ErrorMessage.Contains("Permission"))
                {
                    throw new BadRequest(validationResult.ErrorMessage);
                }
            }
            throw new NotFound($"Hasher Not found with ID: {request.Event.Id}", nameof(Hasher));
        }
        var updatedEvent = _mapper.Map<Domain.Event>(request.Event);
        await _repository.UpdateAsync(updatedEvent);
        return Unit.Value;
    }
}
