using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Domain
{
    public class Hasher : BaseEntity
    {
        public string HashName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string HomeCity { get; set; }
        public string HomeState { get; set; }
        public string HomeCountry { get; set; }

        [ForeignKey(nameof(DietaryChoiceId))]
        public string DietaryChoiceId { get; set; }
        public DietaryChoice Diet { get; set; }

        //TODO need Kennel Implementation to finish
        //[ForeignKey(nameof(MotherKennelId))]
        //public string MotherKennelId { get; set; }
        //public Kennel MotherKennel { get; set; }

        //[ForeignKey(nameof(HomeKennelId))]
        //public string HomeKennelId { get; set; }
        //public Kennel HomeKennel { get; set; }
    }
}
