using FluentValidation;
using OnOut.Application.Contracts;
namespace OnOut.Application.Features.Event.Commands.DeleteEvent;
public class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
{
    private readonly IEventRepository _repository;
    public DeleteEventCommandValidator(IEventRepository repository)
    {
        this._repository = repository;

        RuleFor(q => q.Id)
        .MustAsync(EventExists)
        .WithMessage("Event Does not exist");
    }

    private async Task<bool> EventExists(Guid guid, CancellationToken token)
    {
        return await _repository.ExistsById(guid);
    }
}
