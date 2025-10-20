using OnOut.Application.Features.Kennel;
using OnOut.Domain;
using System.Data.SqlTypes;

namespace OnOut.Application.Features.Event.Queries.GetAll
{
    public class EventListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] PrimaryImage { get; set; } = Array.Empty<byte>();
        public byte[] BannerImage { get; set; } = Array.Empty<byte>();
        public EventTypeDto EventType { get; set; }
        public bool IsSubEvent { get; set; }
        public string Description { get; set; }
        public Address Location { get; set; }

        DateTime TrailTime { get; set; }

        public double ShiggyLevel { get; set; }
        public bool HasKennel { get; set; }
        public KennelDto? EventKennel { get; set; }
        public int Regos { get; set; }
        public SqlMoney EarlyRegoPrice { get; set; }
        public SqlMoney RegoPrice { get; set; }
    }
}