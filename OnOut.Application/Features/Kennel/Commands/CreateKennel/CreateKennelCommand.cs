using MediatR;
using OnOut.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Kennel.Commands.CreateKennel
{
    public class CreateKennelCommand: IRequest<Guid>
    {
        public byte[]? PrimaryImage { get; set; } = Array.Empty<byte>();
        public byte[]? BannerImage { get; set; } = Array.Empty<byte>();
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string? Description { get; set; }
        public KennelType Type { get; set; }
        public DateTime FoundingDate { get; set; } = DateTime.Now;
        public bool ChildFriendly { get; set; } = false;
        public Guid FounderId { get; set; }
    }
}
