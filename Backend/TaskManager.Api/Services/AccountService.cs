using AutoMapper;
using Hashing;
using TaskManager.Api.Common;
using TaskManager.Api.Domain;
using TaskManager.Api.Entities;
using TaskManager.Api.Repositories.Abstract;
using TaskManager.Api.Services.Abstract;
using TaskManager.Api.Services.Strategy.Abstract;

namespace TaskManager.Api.Services;

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
        AccountOperationsResult result = await _accountRepository
            .CreateAccountAsync(_mapper.Map<AccountEntity>(account));

        if (result.Success == false)
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
    public async Task<AuthenticationResult> LoginAsync(string email, string password)
    {
        string passwordHash = Sha256Alghorithm.GenerateHash(password);
        var exists = await _accountRepository.GetAccountByEmailAndPasswordAsync(email, passwordHash);

        if (exists == null)
        {
            return new AuthenticationResult
            {
                AccessToken = null,
                Success = false,
                Errors = new[] { "Invalid email or password" }
            };
        }

        return new AuthenticationResult
        {
            AccessToken = _generatorGwt.GenerateGwt(_mapper.Map<AccountDomain>(exists)),
            Success = true,
            Errors = null
        };
    }
    public async Task<ChangeAccountDataResult> ChangeUsernameAsync(string email, string password, string newUsername)
    {
        string passwordHash = Sha256Alghorithm.GenerateHash(password);
        var result = await _accountRepository.ChangeUsernameAsync(email, passwordHash, newUsername);

        if(result.Success == false)
        {
            return new ChangeAccountDataResult { Success = false, Errors = result.Errors, AccessToken = null };
        }

        return new ChangeAccountDataResult 
        { 
            Success = true, 
            Errors = null, 
            AccessToken = _generatorGwt.GenerateGwt(_mapper.Map<AccountDomain>(result.Account)) 
        };
    }
    public async Task<ChangeAccountDataResult> ChangePasswordAsync(Guid accountId, string oldPassword, string newPassword)
    {
        string oldPasswordHash = Sha256Alghorithm.GenerateHash(oldPassword);
        string newPasswordHash = Sha256Alghorithm.GenerateHash(newPassword);
        var result = await _accountRepository.ChangePasswordAsync(accountId, oldPasswordHash, newPasswordHash);

        if(result.Success == false)
        {
            return new ChangeAccountDataResult
            {
                Success = false,
                Errors = result.Errors,
                AccessToken = null
            };
        }

        return new ChangeAccountDataResult
        {
            Success = true,
            Errors = null,
            AccessToken = _generatorGwt.GenerateGwt(_mapper.Map<AccountDomain>(result.Account))
        };
    }
}