using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Contracts.v1.Requests;

public class AccountLoginRequest
{
    [EmailAddress, Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}