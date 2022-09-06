using Authentication.Common;
using Domain;
using Services.Abstract;
using Entities;
using Hashing;
using Mappers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Repositories.Abstract;
using Services.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Services;

public class AccountService : IAccountService
{
    private readonly ILogger<IAccountService> _logger;
    private readonly IAccountRepository _accountRepository;
    private readonly IOptions<AuthenticationOptions> _options;
    public AccountService(ILogger<IAccountService> logger, IAccountRepository accountRepository, 
        IOptions<AuthenticationOptions> options)
    {
        _logger = logger;
        _accountRepository = accountRepository;
        _options = options;
    }

    public async Task<LoginParameters> LoginAsync(string email, string password)
    {
        try
        {
            string passwordHash = Sha256Alghorithm.GenerateHash(password);

            UserEntity? userEntity = await _accountRepository
                .GetUserByEmailAndPasswordAsync(email, passwordHash);

            if(userEntity == null)
                return new LoginParameters { IsSuccess = false, Jwt = null };

            AccountDomain account = userEntity.ToDomain();

            string jwt = GenerateJwt(account);
            return new LoginParameters { IsSuccess = true, Jwt = jwt };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Can not login user. Something goes wrong");
            throw e;
        }
    }

    public async Task<string> RegisterAsync(AccountDomain account)
    {
        try
        {
            account.Password = Sha256Alghorithm.GenerateHash(account.Password);

            await _accountRepository.CreateUserAsync(account.ToEntity());
            return GenerateJwt(account);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Can not register user. Something goes wrong");
            throw e;
        }
    }

    private string GenerateJwt(AccountDomain account)
    {
        AuthenticationOptions authParams = _options.Value;

        SymmetricSecurityKey securityKey = authParams.GetSymmetricSecurityKey();
        var cridentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, account.Email),
                new Claim(JwtRegisteredClaimNames.Sub, account.Id)
            };

        foreach (var role in account.Roles)
        {
            claims.Add(new Claim("role", role));
        }

        var token = new JwtSecurityToken(authParams.Issuer,
            authParams.Audience,
            claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: cridentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
