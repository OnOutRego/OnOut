namespace OnOut.Application.Features.Rego.Commands.DeleteRego;

public class DeleteRegoCommandHandler : IRequestHandler<DeleteRegoCommand, Unit>
{
    public async Task<Unit> Handle(DeleteRegoCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement handler logic
        return Unit.Value;
    }
}
