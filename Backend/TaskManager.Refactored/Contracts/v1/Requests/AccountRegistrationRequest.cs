using System.ComponentModel.DataAnnotations;

namespace TaskManager.Refactored.Contracts.v1.Requests;

public class AccountRegistrationRequest
{
    [Required, MinLength(5)]
    public string Username { get; set; }
    [EmailAddress, MinLength(5)]
    public string Email { get; set; }
    [MinLength(6)]
    public string Password { get; set; }
}