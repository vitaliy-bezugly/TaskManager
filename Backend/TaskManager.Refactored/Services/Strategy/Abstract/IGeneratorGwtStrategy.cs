using TaskManager.Refactored.Domain;

namespace TaskManager.Refactored.Services.Strategy.Abstract;

public interface IGeneratorGwtStrategy
{
    string GenerateGwt(AccountDomain account);
}
