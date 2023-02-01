using Microsoft.AspNetCore.Mvc;
using TaskManager.Refactored.Contracts.v1;
using TaskManager.Refactored.Contracts.v1.Requests;
using TaskManager.Refactored.Contracts.v1.Responses;
using TaskManager.Refactored.Domain;
using TaskManager.Refactored.Services.Abstract;

namespace TaskManager.Refactored.Controllers.v1;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost, Route(ApiRoutes.Account.Register)]
    public async Task<IActionResult> Register([FromBody] AccountRegistrationRequest request)
    {
        AuthenticationResult result = await _accountService
            .RegisterAsync(new AccountDomain(request.Username, request.Email, request.Password));

        if(result.Success == false)
        {
            return BadRequest(new AuthorizationFailedResponse
            {
                Errors = result.Errors
            });
        }

        return Ok(new AuthorizationSuccessResponse
        {
            access_token = result.AccessToken
        });
    }
    [HttpPost, Route(ApiRoutes.Account.Login)]
    public async Task<IActionResult> Login([FromBody] AccountLoginRequest request)
    {
        AuthenticationResult result = await _accountService.LoginAsync(request.Email, request.Password);

        if (result.Success == false)
        {
            return BadRequest(new AuthorizationFailedResponse
            {
                Errors = result.Errors
            });
        }

        return Ok(new AuthorizationSuccessResponse
        {
            access_token = result.AccessToken
        });
    }
}