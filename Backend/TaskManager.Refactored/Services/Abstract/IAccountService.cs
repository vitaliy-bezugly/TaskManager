using TaskManager.Refactored.Domain;

namespace TaskManager.Refactored.Services.Abstract;

public interface IAccountService
{
    Task<AuthenticationResult> RegisterAsync(AccountDomain account);
    Task<AuthenticationResult> LoginAsync(string email, string password);
    Task<ChangeAccountDataResult> ChangeUsernameAsync(string email, string password, string newUsername);
    Task<ChangeAccountDataResult> ChangePasswordAsync(Guid accountId, string oldPassword, string newPassword);
}