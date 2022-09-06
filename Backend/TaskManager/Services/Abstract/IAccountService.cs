using Domain;
using Services.Data;

namespace Services.Abstract;

public interface IAccountService
{
    public Task<LoginParameters> LoginAsync(string email, string password);
    public Task<string> RegisterAsync(AccountDomain account);
}