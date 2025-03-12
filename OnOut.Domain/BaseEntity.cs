namespace OnOut.Domain
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
