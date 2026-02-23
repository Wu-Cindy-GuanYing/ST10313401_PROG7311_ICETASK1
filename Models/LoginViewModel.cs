using System.ComponentModel.DataAnnotations;

namespace CV_Online_ST10313401.Models;

public class LoginViewModel
{
    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";

    public string? ReturnUrl { get; set; }
}