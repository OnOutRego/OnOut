using MediatR;
namespace OnOut.Application.Features.Event.Commands.CreateEvent;
public class CreateEventCommand : IRequest<Guid>
{
    public Guid CreatorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EventTypeName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LocationName { get; set; } = string.Empty;
    public DateTime TrailTime { get; set; }
    public double ShiggyLevel { get; set; }
    public Guid? KennelId { get; set; }
    public int MaxRegos { get; set; }
    public bool HasGimmes { get; set; }
    public List<string> Gimmes { get; set; } = [];
    public bool HasEarlyRego { get; set; }
    public DateTime EarlyRegoDeadline { get; set; }
    public decimal EarlyRegoPrice { get; set; }
    public decimal RegoPrice { get; set; }
    public DateTime RegoDeadline { get; set; }
    public byte[] PrimaryImage { get; set; } = Array.Empty<byte>();
    public byte[] BannerImage { get; set; } = Array.Empty<byte>();
}
