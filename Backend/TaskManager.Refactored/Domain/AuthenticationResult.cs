namespace TaskManager.Api.Domain;

public class AuthenticationResult
{
    public string? AccessToken { get; set; }
    public bool Success { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}
