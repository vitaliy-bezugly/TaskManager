using Domain;
using Entities;
using TaskManager.ViewModels;

namespace Mappers;

public static class AccountMapper
{
    public static AccountDomain ToDomain(this UserEntity user)
    {
        return new AccountDomain { Id = user.Id, Email = user.Email, Password = user.Password, 
            Username = user.Username, Roles = user.Roles?.Select(x => x.Role).ToList() };
    }
    public static AccountDomain ToDomain(this RegisterViewModel registerViewModel)
    {
        return new AccountDomain { Email = registerViewModel.Email, Password = registerViewModel.Password, 
            Username = registerViewModel.Username, Roles = registerViewModel.Roles };
    }
    public static UserEntity ToEntity(this AccountDomain user)
    {
        return new UserEntity { Id = user.Id, Email = user.Email, Password = user.Password, 
            Username = user.Username, Roles = user.Roles?.Select(x => x.ToEntity()).ToList() };
    }
}
