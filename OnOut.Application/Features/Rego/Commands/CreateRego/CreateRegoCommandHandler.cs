using MediatR;

namespace OnOut.Application.Features.Rego.Commands.CreateRego;

public class CreateRegoCommandHandler : IRequestHandler<CreateRegoCommand, Unit>
{
    public async Task<Unit> Handle(CreateRegoCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement handler logic
        return Unit.Value;
    }
}
