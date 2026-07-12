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

        [ForeignKey(nameof(DietaryChoiceId))]
        public string DietaryChoiceId { get; set; }
        public DietaryChoice Diet { get; set; }

        //TODO need Kennel Implementation to finish
        [ForeignKey(nameof(MotherKennelId))]
        public string MotherKennelId { get; set; }
        public Kennel MotherKennel { get; set; }

        [ForeignKey(nameof(HomeKennelId))]
        public string HomeKennelId { get; set; }
        public Kennel HomeKennel { get; set; }

        public bool ProfileCompleted { get; set; }
    }
}
