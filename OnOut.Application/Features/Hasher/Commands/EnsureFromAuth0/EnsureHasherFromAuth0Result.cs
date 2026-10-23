public class EnsureHasherFromAuth0Result
{
    public Guid HasherId { get; set; }
    public bool IsNewUser { get; set; }
    public bool NeedsProfileCompletion { get; set; }
    public string HomeCity { get; set; } = string.Empty;
    public string HomeState { get; set; } = string.Empty;
    public string HomeCountry { get; set; } = string.Empty;
}