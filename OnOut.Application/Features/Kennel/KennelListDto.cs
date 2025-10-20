using OnOut.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Kennel
{
    public class KennelListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public bool ChildFriendly { get; set; } = false;
        public int NumMembers { get; set; }
        public int NumEvents { get; set; }
        public KennelType Type { get; set; }
    }
}
