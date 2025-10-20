using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Domain
{
    public class KennelRoles: BaseEntity
    {
        [ForeignKey(nameof(KennelId))]
        public Guid KennelId { get; set; }
        public Kennel Kennel { get; set; }

    }
}
