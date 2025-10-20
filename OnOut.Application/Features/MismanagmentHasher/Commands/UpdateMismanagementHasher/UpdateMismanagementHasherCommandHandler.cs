using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using OnOut.Application.Features.MismanagmentHasher.Commands.UpdateMismanagementHasher;
using OnOut.Domain;

public class UpdateMismanagementHasherCommandHandler : IRequestHandler<UpdateMismanagementHasherCommand, Unit>
{
    private readonly IAppLogger<UpdateMismanagementHasherCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateMismanagementHasherCommand> _validator;
    private readonly IMismanagmentHashersRepository _repository;

    public UpdateMismanagementHasherCommandHandler(
        IAppLogger<UpdateMismanagementHasherCommandHandler> logger,
        IMapper mapper,
        IValidator<UpdateMismanagementHasherCommand> validator,
        IMismanagmentHashersRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateMismanagementHasherCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for UpdateMismanagementHasherCommand: {Errors}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        var entity = await _repository.GetByIdAsync(request.MismanagmentHasherId);
        if (entity == null)
        {
            _logger.LogWarning("MismanagementHasher with Id {Id} not found.", request.MismanagmentHasherId);
            throw new KeyNotFoundException($"MismanagementHasher with Id {request.MismanagmentHasherId} not found.");
        }

        entity.Name = request.Name;
        await _repository.UpdateAsync(entity);

        _logger.LogInformation("MismanagementHasher with Id {Id} updated successfully.", request.MismanagmentHasherId);

        return Unit.Value;
    }
}