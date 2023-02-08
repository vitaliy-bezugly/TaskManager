using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Contracts.v1.Requests;

public class AccountChangeUsernameRequest
{
    [Required]
    public string NewUsername { get; set; }
    [Required, MinLength(6)]
    public string CurrentPassword { get; set; }
}
