using MediatR;

namespace OnOut.Application.Features.MismanagmentHasher.Queries
{
    public class GetByKennelQuery : IRequest<List<MismanagementHasherDto>>
    {
        public Guid KennelId { get; set; }
    }
}