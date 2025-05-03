using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OnOut.Domain;

namespace OnOut.Application.Features.Kennel.Commands.UpdateKennel
{
    public class UpdateKennelCommand: IRequest<Unit>
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
    }
}
