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
            .NotNull().WithMessage("Must Have an Event Name");
        //RuleFor(q => q.Name)
        //    .MustAsync(EventExists).WithMessage("An Event Already Exists with that name");
        RuleFor(q => q.EventKennel.Id)
            .MustAsync(KennelExists)
             .When(q => q.HasKennel)
              .WithMessage("Marked as Kennel Event, Kennel does not exist");
    }

    private async Task<bool> KennelExists(Guid guid, CancellationToken token)
    {
        return await _kennelRepository.ExistsById(guid);
    }

    private async Task<bool> EventExists(string name, CancellationToken token)
    {
        var exists = await _repository.ExistsByName(name);
        return !exists;
    }
}
