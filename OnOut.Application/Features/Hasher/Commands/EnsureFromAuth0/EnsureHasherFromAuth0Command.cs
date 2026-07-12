using MediatR;

public class EnsureHasherFromAuth0Command : IRequest<EnsureHasherFromAuth0Result>
{
    public string Subject { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Name { get; set; }
}