using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Domain
{
    public class Event: BaseEntity
    {
        [ForeignKey(nameof(EventTypeId))]
        public Guid EventTypeId { get; set; }
        public EventType EventType { get; set; }


        public bool IsSubEvent { get; set; }
        public string Description { get; set; }
        
        [ForeignKey(nameof(LocationId))]
        public Guid LocationId { get; set; }
        public Address Location { get; set; }

        DateTime TrailTime { get; set; }

        public double ShiggyLevel { get; set; }

        #region KennelInfo
        public bool HasKennel { get; set; }

        [ForeignKey(nameof(KennelId))]
        public Guid? KennelId { get; set; }
        //public Kennel EventKennel


        #endregion

        #region HasherStuff
        public int MaxRegos { get; set; }

        public List<string> Gimmes { get; set; }
        public bool HasGimmes { get; set; }

        public virtual IList<Rego> Regos { get; set; }

        public bool HasEarlyRego { get; set; }
        public DateTime EarlyRegoDeadline { get; set; }
        public SqlMoney EarlyRegoPrice { get; set; }
        public SqlMoney RegoPrice { get; set; }
        public DateTime RegoDeadline { get; set; }

        #endregion

    }
}
