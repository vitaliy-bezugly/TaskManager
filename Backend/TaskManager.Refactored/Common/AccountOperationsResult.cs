using TaskManager.Refactored.Entities;

namespace TaskManager.Refactored.Common;

public class AccountOperationsResult
{
    public bool Success { get; set; }
    public string[]? Errors { get; set; }
    public AccountEntity? Account { get; set; }
}
