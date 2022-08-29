using System.ComponentModel.DataAnnotations;

namespace TaskManager.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Username { get; set; }
    public List<string>? Roles { get; set; }
}