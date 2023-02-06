﻿using AutoMapper;
using Hashing;
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
    public async Task<ChangeAccountDataResult> ChangeUsername(string email, string password, string newUsername)
    {
        string passwordHash = Sha256Alghorithm.GenerateHash(password);
        var result = await _accountRepository.ChangeUsername(email, passwordHash, newUsername);

        if(result.Success == false)
        {
            return new ChangeAccountDataResult { Success = false, Errors = result.Errors };
        }

        return new ChangeAccountDataResult { Success = true, Errors = null };
    }
}
