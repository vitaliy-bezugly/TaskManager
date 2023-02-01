namespace TaskManager.Refactored.Contracts.v1.Responses;

public class AuthorizationFailedResponse
{
    public IEnumerable<string> Errors { get; set; }
}
