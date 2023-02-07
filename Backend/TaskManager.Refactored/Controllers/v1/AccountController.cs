using Microsoft.AspNetCore.Authorization;
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
    private readonly IClaimParser _claimParser;
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService, IClaimParser claimParser)
    {
        _accountService = accountService;
        _claimParser = claimParser;
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
    [HttpPut, Authorize, Route(ApiRoutes.Account.ChangeUsername)]
    public async Task<IActionResult> ChangeUsername([FromBody] AccountChangeUsernameRequest request)
    {
        ChangeAccountDataResult result = await _accountService.ChangeUsernameAsync(_claimParser.GetEmail(), 
            request.CurrentPassword, request.NewUsername);

        if(result.Success == false)
        {
            return BadRequest(new ChangeUsernameFailedResponse
            {
                Errors = result.Errors
            });
        }

        return Ok(new ChangeUsernameSuccessResponse { Username = request.NewUsername });
    }
    [HttpPut, Authorize, Route(ApiRoutes.Account.ChangePassword)]
    public async Task<IActionResult> ChangePassword([FromBody] AccountChangePasswordRequest request)
    {
        var result = await _accountService.ChangePasswordAsync(_claimParser.GetUserId(), 
            request.OldPassword, request.NewPassword);

        if (result.Success == false)
        {
            return BadRequest(new ChangePasswordFailedResponse
            {
                Errors = result.Errors
            });
        }

        return Ok(new ChangePasswordSuccessResponse { NewPassword = request.NewPassword });
    }
}