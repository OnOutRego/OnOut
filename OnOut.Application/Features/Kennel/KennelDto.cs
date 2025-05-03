using OnOut.Application.Features.Event.Queries.GetAll;
using OnOut.Application.Features.Hasher.Queries.GetWithDetails;
using OnOut.Domain;

namespace OnOut.Application.Features.Kennel
{
    public class KennelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] PrimaryImage { get; set; } = Array.Empty<byte>();
        public byte[] BannerImage { get; set; } = Array.Empty<byte>();
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string MeetingTimes { get; set; }
        public KennelType Type { get; set; }
        public Status Status { get; set; } = 0;
        public List<KennelRolesDto> Roles { get; set; }
        public List<HasherDetailsDto> Members { get; set; }
        public List<MisManagmentHashersDto> MisManagement { get; set; }
        public List<EventListDto> Events { get; set; }

    }
}