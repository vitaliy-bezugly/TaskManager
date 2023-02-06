namespace TaskManager.Refactored.Domain;

public class ChangeAccountDataResult
{
    public bool Success { get; set; }
    public string[]? Errors { get; set; }
}