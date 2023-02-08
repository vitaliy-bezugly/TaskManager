using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManager.Api.Common;
using TaskManager.Api.Domain;
using TaskManager.Api.Services.Strategy.Abstract;

namespace TaskManager.Api.Services.Strategy;

public class DefaultGeneratorGwtStrategy : IGeneratorGwtStrategy
{
    private readonly IOptions<AuthenticationOptions> _options;

    public DefaultGeneratorGwtStrategy(IOptions<AuthenticationOptions> options)
    {
        _options = options;
    }

    public string GenerateGwt(AccountDomain account)
    {
        AuthenticationOptions authParams = _options.Value;

        SymmetricSecurityKey securityKey = authParams.GetSymmetricSecurityKey();
        var cridentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.GivenName, account.Username),
            new Claim(JwtRegisteredClaimNames.Email, account.Email),
            new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString())
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
