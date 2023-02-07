using System.ComponentModel.DataAnnotations;

namespace TaskManager.Refactored.Contracts.v1.Requests;

public class AccountChangePasswordRequest
{
    [Required]
    public string OldPassword { get; set; }
    [Required, MinLength(6)]
    public string NewPassword { get; set; }
}
