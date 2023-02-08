using TaskManager.Api.Common;
using TaskManager.Api.Entities;

namespace TaskManager.Api.Repositories.Abstract;

public interface IAccountRepository
{
    Task<AccountOperationsResult> CreateAccountAsync(AccountEntity account);
    Task<AccountEntity?> GetAccountByEmailAndPasswordAsync(string email, string passwordHash);
    Task<AccountOperationsResult> ChangeUsernameAsync(string email, string passwordHash, string newUsername);
    Task<AccountOperationsResult> ChangePasswordAsync(Guid accountId, string oldPasswordHash, string newPasswordHash);
}