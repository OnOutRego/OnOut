using AutoMapper;
using OnOut.Application.Features.Event.Queries.GetAll;
using OnOut.Application.Features.Event;
using OnOut.Domain;
namespace OnOut.Application.MappingProfiles;
public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<Event, EventListDto>().ReverseMap();
        CreateMap<EventType, EventTypeDto>().ReverseMap();
        CreateMap<Event, EventDto>().ReverseMap();
    }
}
