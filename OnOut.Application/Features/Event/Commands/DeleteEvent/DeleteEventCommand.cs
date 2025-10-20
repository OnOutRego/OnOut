using MediatR;
namespace OnOut.Application.Features.Event.Commands.DeleteEvent;
public class DeleteEventCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
