namespace Domain.Services.Abstract;

public interface IAccountService
{
    public Task<Tuple<bool, string>> LoginAsync(string email, string password);
    public Task<string> RegisterAsync();
}