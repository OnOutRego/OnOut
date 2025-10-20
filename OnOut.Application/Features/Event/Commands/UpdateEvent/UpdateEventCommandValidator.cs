using FluentValidation;
using OnOut.Application.Contracts;
namespace OnOut.Application.Features.Event.Commands.UpdateEvent;
public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
{
    private readonly IEventRepository _repository;
    public UpdateEventCommandValidator(IEventRepository repository)
    {
        this._repository = repository;
        RuleFor(q => q)
            .MustAsync(EventExists)
             .WithMessage("Event Does not Exist!");
        RuleFor(q => q.SenderId)
            .MustAsync(PermissionsExist)
             .WithMessage("You do not have permission to edit this Event");

    }

    private async Task<bool> PermissionsExist(Guid guid, CancellationToken token)
    {
        //TODO: Authentication will be required to check claims IF the editor is NOT the hasher
        return true;
    }

    private async Task<bool> EventExists(UpdateEventCommand command, CancellationToken token)
    {
        return await _repository.ExistsById(command.Event.Id);
    }
}
