using Domain.Services.Abstract;

namespace Services;

public class AccountService : IAccountService
{
    public Task<Tuple<bool, string>> LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<string> RegisterAsync()
    {
        throw new NotImplementedException();
    }
}
