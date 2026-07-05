using MediatR;

namespace OnOut.Application.Features.Rego.Queries.GetAllRego;

public class GetAllRegoQueryHandler : IRequestHandler<GetAllRegoQuery, RegoDto>
{
    public async Task<RegoDto> Handle(GetAllRegoQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query logic
        return new RegoDto();
    }
}
