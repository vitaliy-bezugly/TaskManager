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

    public async Task<CreationAccountResult> CreateAccountAsync(AccountEntity account)
    {
        var existingUser = await FindByEmailAsync(account.Email);

        if(existingUser != null)
        {
            return new CreationAccountResult
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

        return new CreationAccountResult
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

    public async Task<AccountEntity?> FindByEmailAsync(string email)
    {
        return await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email);
    }
}