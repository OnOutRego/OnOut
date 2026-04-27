namespace OnOut.Application.Features.Rego.Commands.UpdateRego;

public class UpdateRegoCommandHandler : IRequestHandler<UpdateRegoCommand, Unit>
{
    public async Task<Unit> Handle(UpdateRegoCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement handler logic
        return Unit.Value;
    }
}
