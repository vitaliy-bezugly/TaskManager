using Services.Abstract;
using Mappers;
using Microsoft.AspNetCore.Mvc;
using Services.Data;
using TaskManager.ViewModels;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService, ILogger<AccountController> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_accountService.GetAccounts().Select(x => new LoginViewModel
        {
            Email = x.Email,
            Password = x.Password
        }));
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel loginData)
    {
        try
        {
            LoginParameters loginResult = await _accountService.LoginAsync(loginData.Email, 
                loginData.Password);

            if(loginResult.IsSuccess == false)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                access_token = loginResult.Jwt
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Can not login user");
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel registerData)
    {
        try
        {
            string jwt = await _accountService.RegisterAsync(registerData.ToDomain());

            return Ok(new
            {
                access_token = jwt
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Can not register user");
            return BadRequest();
        }
    }
}
