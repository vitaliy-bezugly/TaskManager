using Entities;

namespace Persistence.Repositories.Abstract;

public interface IAccountRepository
{
    public Task<UserEntity?> GetUserByEmailAndPasswordAsync(string email, string password);
    public Task CreateUserAsync(UserEntity user);
    public Task DeleteUserAsync(string userId);
    public Task UpdateUserAsync(string userId, UserEntity user);
    public IEnumerable<UserEntity> GetAllUsers();
}
