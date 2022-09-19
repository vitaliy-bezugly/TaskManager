using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;

namespace TaskManager.IntegrationTests.ControllerTests;

public class AccountControllerTests : AccountTest
{
    private const string _key = "secretkey2308012341+-";

    [Test]
    public async Task Login_LoginData_OkResponseCorrectJwt()
    {
        HttpResponseMessage response = await LoginAsync();
        CheckStatusCodeIfNotValidFailTest(response);

        await VerifyJwtFromResponseAsync(response);
    }
    [Test]
    public async Task Register_CreateUser_OkResponseCorrectJwt()
    {
        HttpResponseMessage response = await CreateUserAsync();
        CheckStatusCodeIfNotValidFailTest(response);

        await VerifyJwtFromResponseAsync(response);
    }

    private async Task VerifyJwtFromResponseAsync(HttpResponseMessage response)
    {
        string jwt = await GetJwtFromResponseAsync(response);
        bool isCorrect = ValidateJwt(jwt);

        if (isCorrect == true)
            Assert.Pass();
        Assert.Fail("Jwt is not correct");
    }

    private bool ValidateJwt(string jwt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();

        SecurityToken validatedToken;
        IPrincipal principal = tokenHandler.ValidateToken(jwt, validationParameters, out validatedToken);
        return true;
    }
    private static TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters()
        {
            ValidateLifetime = false, // Because there is no expiration in the generated token
            ValidateAudience = false, // Because there is no audiance in the generated token
            ValidateIssuer = false,   // Because there is no issuer in the generated token
            ValidIssuer = "Sample",
            ValidAudience = "Sample",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)) // The same key as the one that generate the token
        };
    }
}
