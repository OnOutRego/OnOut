using System.ComponentModel.DataAnnotations.Schema;

namespace OnOut.Domain
{
    public class KennelMember:BaseEntity
    {
        [ForeignKey(nameof(HasherId))]
        public Guid HasherId { get; set; }
        public Hasher Hasher { get; set; }

        [ForeignKey(nameof(KennelId))]
        public Guid KennelId { get; set; }
        public Kennel Kennel { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Guid RoleId { get; set; }
        public KennelRoles Role { get; set; }
    }
}