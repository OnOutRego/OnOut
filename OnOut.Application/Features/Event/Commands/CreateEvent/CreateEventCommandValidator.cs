using FluentValidation;
using OnOut.Application.Contracts;
namespace OnOut.Application.Features.Event.Commands.CreateEvent;
public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    private readonly IEventRepository _repository;
    private readonly IKennelRepository _kennelRepository;

    public CreateEventCommandValidator(IEventRepository repository, IKennelRepository kennelRepository)
    {
        this._repository = repository;
        this._kennelRepository = kennelRepository;
        RuleFor(q => q.Name)
            .NotEmpty().WithMessage("Must Have an Event Name");
        RuleFor(q => q.EventTypeName)
            .NotEmpty().WithMessage("Must Have an Event Type");
        RuleFor(q => q.LocationName)
            .NotEmpty().WithMessage("Must Have a Location");
        RuleFor(q => q.TrailTime)
            .GreaterThan(DateTime.UtcNow).WithMessage("Event date must be in the future");
        RuleFor(q => q.RegoDeadline)
            .LessThanOrEqualTo(q => q.TrailTime).WithMessage("Registration deadline must be on or before the event date");
        RuleFor(q => q.RegoPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative");
        RuleFor(q => q.KennelId)
            .MustAsync(async (kennelId, token) => kennelId.HasValue && await _kennelRepository.ExistsById(kennelId.Value))
            .When(q => q.KennelId.HasValue)
            .WithMessage("Hosting kennel does not exist");
        //RuleFor(q => q.Name)
        //    .MustAsync(EventExists).WithMessage("An Event Already Exists with that name");
    }

    private async Task<bool> EventExists(string name, CancellationToken token)
    {
        var exists = await _repository.ExistsByName(name);
        return !exists;
    }
}
