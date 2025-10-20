using MediatR;
namespace OnOut.Application.Features.Event.Queries.GetWithDetails;
public class GetOneWithDetailsQuery : IRequest<EventDto>
{
    public Guid EventId { get; set; }
}
