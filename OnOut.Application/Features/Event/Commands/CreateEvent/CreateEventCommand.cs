using MediatR;
using OnOut.Application.Features.Hasher.Queries.GetWithDetails;
using OnOut.Application.Features.Event.Queries.GetAll;
using OnOut.Domain;
using OnOut.Application.Features.Kennel;
using System.Data.SqlTypes;
namespace OnOut.Application.Features.Event.Commands.CreateEvent;
public class CreateEventCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public HasherDetailsDto Creator { get; set; }
    public EventTypeDto EventType { get; set; }
    public bool IsSubEvent { get; set; }
    public string Description { get; set; }
    public Address Location { get; set; }
    public DateTime TrailTime { get; set; }
    public double ShiggyLevel { get; set; }
    public bool HasKennel { get; set; }
    public KennelDto? EventKennel { get; set; }
    public int MaxRegos { get; set; }
    public List<string> Gimmes { get; set; }
    public bool HasGimmes { get; set; }
    public virtual IList<Rego> Regos { get; set; }
    public bool HasEarlyRego { get; set; }
    public DateTime EarlyRegoDeadline { get; set; }
    public SqlMoney EarlyRegoPrice { get; set; }
    public SqlMoney RegoPrice { get; set; }
    public DateTime RegoDeadline { get; set; }
}
