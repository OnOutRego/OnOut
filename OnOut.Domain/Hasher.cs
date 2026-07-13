using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Domain
{
    public class Hasher : ProfileEntity
    {
        public string HashName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        // New: link to Auth0 identity
        public string Auth0Subject { get; set; } = string.Empty;

        public string HomeCity { get; set; } = string.Empty;
        public string HomeState { get; set; } = string.Empty;
        public string HomeCountry { get; set; } = string.Empty;

        public Guid? DietaryChoiceId { get; set; }
        [ForeignKey(nameof(DietaryChoiceId))]
        public DietaryChoice? Diet { get; set; }

        //TODO need Kennel Implementation to finish
        public Guid? MotherKennelId { get; set; }
        [ForeignKey(nameof(MotherKennelId))]
        public Kennel? MotherKennel { get; set; }

        public Guid? HomeKennelId { get; set; }
        [ForeignKey(nameof(HomeKennelId))]
        public Kennel? HomeKennel { get; set; }

        public bool ProfileCompleted { get; set; }
    }
}
