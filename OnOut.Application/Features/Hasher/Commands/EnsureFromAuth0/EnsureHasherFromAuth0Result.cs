public class EnsureHasherFromAuth0Result
{
    public Guid HasherId { get; set; }
    public bool IsNewUser { get; set; }
    public bool NeedsProfileCompletion { get; set; }
}