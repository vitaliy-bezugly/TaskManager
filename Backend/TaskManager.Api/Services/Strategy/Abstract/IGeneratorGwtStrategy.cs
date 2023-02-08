using TaskManager.Api.Domain;

namespace TaskManager.Api.Services.Strategy.Abstract;

public interface IGeneratorGwtStrategy
{
    string GenerateGwt(AccountDomain account);
}
