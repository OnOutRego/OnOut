using System.ComponentModel.DataAnnotations.Schema;

namespace OnOut.Domain
{
    public class Rego:BaseEntity
    {
        [ForeignKey(nameof(HasherId))]
        public Guid HasherId { get; set; }
        public Hasher Hasher { get; set; }

        [ForeignKey(nameof(EventId))]
        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public bool IsPaid { get; set; }

        public bool IsTransferable { get; set; }

        public bool IsRefundable { get; set; }


    }

}