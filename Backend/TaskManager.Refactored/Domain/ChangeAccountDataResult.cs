namespace TaskManager.Api.Domain;

public class ChangeAccountDataResult
{
    public bool Success { get; set; }
    public string[]? Errors { get; set; }
    public string? AccessToken { get; set; }
}