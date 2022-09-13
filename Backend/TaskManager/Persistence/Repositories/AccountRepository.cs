using Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Abstract;

namespace Persistence.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _context;
    public AccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateUserAsync(UserEntity user)
    {
        if(user == null)
            throw new ArgumentNullException(nameof(user) + "can not be null");

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(string userId)
    {
        if (userId == null)
            throw new ArgumentNullException(nameof(userId) + "can not be null");

        var userToDelete = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (userToDelete == null)
            throw new ArgumentException("There is no user witd id: {userId}", userId);

        _context.Remove(userToDelete);
        await _context.SaveChangesAsync();
    }

    public IEnumerable<UserEntity> GetAllUsers()
    {
        return _context.Users;
    }

    public async Task<UserEntity?> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        if (email == null || string.IsNullOrEmpty(password) == true)
            throw new ArgumentException($"Email({email}), is null or password({password}) " +
                $"is null or empty");

        var user = await _context.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Email == email &&
            x.Password == password);

        return user;
    }

    public async Task UpdateUserAsync(string userId, UserEntity user)
    {
        if (userId == null || user == null)
            throw new ArgumentException($"User id({userId}) or user({user}) is null");

        var userToUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (userToUpdate == null)
            throw new ArgumentException($"There is no user with id: {userId}");

        userToUpdate.Username = user.Username;
        userToUpdate.Email = user.Email;
        userToUpdate.Roles = user.Roles;
        userToUpdate.Tasks = user.Tasks;

        await _context.SaveChangesAsync();
    }
}
