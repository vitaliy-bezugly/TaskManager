using AutoMapper;
using TaskManager.Refactored.Common;
using TaskManager.Refactored.Domain;
using TaskManager.Refactored.Entities;
using TaskManager.Refactored.Repositories.Abstract;
using TaskManager.Refactored.Services.Abstract;
using TaskManager.Refactored.Services.Strategy.Abstract;

namespace TaskManager.Refactored.Services;

public class AccountService : IAccountService
{
    private readonly IGeneratorGwtStrategy _generatorGwt;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    public AccountService(IAccountRepository accountRepository, IMapper mapper, IGeneratorGwtStrategy generatorGwt)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _generatorGwt = generatorGwt;
    }

    public async Task<AuthenticationResult> RegisterAsync(AccountDomain account)
    {
        CreationAccountResult result = await _accountRepository
            .CreateAccountAsync(_mapper.Map<AccountEntity>(account));

        if(result.Success == false)
        {
            return new AuthenticationResult
            {
                AccessToken = null,
                Success = false,
                Errors = result.Errors
            };
        }

        return new AuthenticationResult
        {
            AccessToken = _generatorGwt.GenerateGwt(account),
            Success = true,
            Errors = null
        };
    }

    public Task<AuthenticationResult> LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }
}
