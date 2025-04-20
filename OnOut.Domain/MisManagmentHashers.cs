using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Domain
{
    public class MisManagmentHashers:BaseEntity
    {
        [ForeignKey(nameof(HasherId))]
        public Guid HasherId { get; set; }
        public Hasher Hasher { get; set; }

        [ForeignKey(nameof(KennelId))]
        public Guid KennelId { get; set;}
        public Kennel Kennel { get; set;}

        public string Position { get; set; }
    }
}
