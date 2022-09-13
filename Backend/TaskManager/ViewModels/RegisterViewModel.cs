using System.ComponentModel.DataAnnotations;

namespace TaskManager.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string? Username { get; set; }
    [Required, EmailAddress]
    public string? Email { get; set; }
    public string? Password { get; set; }
    public List<string>? Roles { get; set; }
}