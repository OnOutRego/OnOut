using MediatR;

namespace OnOut.Application.Features.Rego.Queries.GetRego;

public class GetRegoQueryHandler : IRequestHandler<GetRegoQuery, RegoDto>
{
    public async Task<RegoDto> Handle(GetRegoQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement query logic
        return new RegoDto();
    }
}
