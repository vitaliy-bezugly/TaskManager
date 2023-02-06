using TaskManager.Refactored.Common;
using TaskManager.Refactored.Entities;

namespace TaskManager.Refactored.Repositories.Abstract;

public interface IAccountRepository
{
    Task<AccountOperationsResult> CreateAccountAsync(AccountEntity account);
    Task<AccountEntity?> GetAccountByEmailAndPasswordAsync(string email, string passwordHash);
    Task<AccountOperationsResult> ChangeUsername(string email, string passwordHash, string newUsername);
}