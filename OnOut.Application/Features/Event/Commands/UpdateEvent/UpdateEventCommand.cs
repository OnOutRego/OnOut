using MediatR;
using OnOut.Application.Features.Event;
namespace OnOut.Application.Features.Event.Commands.UpdateEvent;
public class UpdateEventCommand : IRequest<Unit>
{
    public EventDto Event { get; set; }
    public Guid SenderId { get; set; }
}
