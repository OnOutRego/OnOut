using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace OnOut.Domain
{
    public class Kennel : ProfileEntity
    {
        public string? Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string? MeetingTimes { get; set; }
        public KennelType Type { get; set; }
        public Status Status { get; set; } = 0;
        public virtual IList<KennelRoles> Roles { get; set; }
        public DateTime FoundingDate { get; set; }
        public bool ChildFriendly { get; set; } = false;

        #region Hashers
        public virtual IList<KennelMember> Members { get; set; }
        public virtual IList<MisManagmentHashers> Mismanagement { get; set; }
        #endregion
        #region Events
        public virtual IList<Event> Events { get; set; }
        #endregion


    }
    public enum KennelType
    {
        Fixed,
        Travel,
        Virtual
    }

    public enum Status
    {
        Active,
        Idle,
        Retired
    }

}