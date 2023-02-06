using Microsoft.EntityFrameworkCore;
using TaskManager.Refactored.Common;
using TaskManager.Refactored.Entities;
using TaskManager.Refactored.Persistence;
using TaskManager.Refactored.Repositories.Abstract;

namespace TaskManager.Refactored.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDataContext _context;

    public AccountRepository(ApplicationDataContext context)
    {
        _context = context;
    }

    public async Task<AccountOperationsResult> CreateAccountAsync(AccountEntity account)
    {
        var existingUser = await FindByEmailAsync(account.Email);

        if(existingUser != null)
        {
            return new AccountOperationsResult
            {
                Success = false,
                Errors = new string[]
                {
                    "User with this email address already exist"
                }
            };
        }

        await _context.Accounts.AddAsync(account);
        int updated = await _context.SaveChangesAsync();

        return new AccountOperationsResult
        {
            Success = updated > 0,
            Errors = new string[]
            {
                "Can not create an account. Something goes wrong with database"
            }
        };
    }
    public async Task<AccountEntity?> GetAccountByEmailAndPasswordAsync(string email, string password)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
    }
    public async Task<AccountOperationsResult> ChangeUsername(string email, string passwordHash, string newUsername)
    {
        var account = await GetAccountByEmailAndPasswordAsync(email, passwordHash);

        if (account == null)
        {
            return new AccountOperationsResult
            {
                Success = false,
                Errors = new string[]
                {
                    "User with given email address and password does not exist"
                }
            };
        }

        account.Username = newUsername;
        int updated = await _context.SaveChangesAsync();

        return updated > 0 ? new AccountOperationsResult { Success = true, Errors = null } : 
            new AccountOperationsResult { Success = false, Errors = new[] { "Can not update account data" } };
    }
    public async Task<AccountEntity?> FindByEmailAsync(string email)
    {
        return await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email);
    }
}