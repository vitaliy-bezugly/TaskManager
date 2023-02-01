using Hashing;
using TaskManager.Refactored.Domain.Abstract;

namespace TaskManager.Refactored.Domain;

public class AccountDomain : BaseDomain
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string[] Roles { get; set; }

    public AccountDomain(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = Sha256Alghorithm.GenerateHash(password);
        Roles = new string[] { "user" };
    }
    public AccountDomain(string username, string email, string password, string[] roles)
    {
        Username = username;
        Email = email;
        Password = Sha256Alghorithm.GenerateHash(password);
        Roles = roles;
    }

    public void ChangePassword(string newPassword)
    {
        Password = Sha256Alghorithm.GenerateHash(newPassword);
    }
}