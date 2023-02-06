using TaskManager.Refactored.Domain;

namespace TaskManager.Refactored.Services.Abstract;

public interface IAccountService
{
    Task<AuthenticationResult> RegisterAsync(AccountDomain account);
    Task<AuthenticationResult> LoginAsync(string email, string password);
    Task<ChangeAccountDataResult> ChangeUsername(string email, string password, string newUsername);
}
