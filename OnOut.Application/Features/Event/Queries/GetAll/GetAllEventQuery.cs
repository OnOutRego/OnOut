using MediatR;

namespace OnOut.Application.Features.Event.Queries.GetAll
{
    public class GetAllEventQuery:IRequest<List<EventListDto>>
    {
    }
}