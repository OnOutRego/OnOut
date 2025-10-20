using MediatR;
using AutoMapper;
using FluentValidation;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using OnOut.Application.Exceptions;

namespace OnOut.Application.Features.Event.Commands.DeleteEvent;
public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Unit>
{
    private readonly IEventRepository _repository;
    private readonly IAppLogger<DeleteEventCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<DeleteEventCommand> _validator;

    public DeleteEventCommandHandler(IEventRepository repository, IAppLogger<DeleteEventCommandHandler> logger, IMapper mapper, IValidator<DeleteEventCommand> validator)
    {
        this._repository = repository;
        this._logger = logger;
        this._mapper = mapper;
        this._validator = validator;
    }


    public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
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
        var requestedEvent = await _repository.GetByIdAsync(request.Id);
        await _repository.DeleteAsync(requestedEvent);
        return Unit.Value;
    }
}
