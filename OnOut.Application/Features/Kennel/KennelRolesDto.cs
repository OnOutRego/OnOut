namespace OnOut.Application.Features.Kennel
{
    public class KennelRolesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public KennelDto Kennel { get; set; }
    }
}