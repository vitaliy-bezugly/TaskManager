using TaskManager.Refactored.Common;
using TaskManager.Refactored.Entities;

namespace TaskManager.Refactored.Repositories.Abstract;

public interface IAccountRepository
{
    Task<CreationAccountResult> CreateAccountAsync(AccountEntity account);
    Task<AccountEntity?> GetAccountByEmailAndPasswordAsync(string email, string passwordHash);
}