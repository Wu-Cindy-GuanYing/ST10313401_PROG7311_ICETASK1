using System.ComponentModel.DataAnnotations;

namespace CV_Online_ST10313401.Models;

public class CvProfile
{
    public int Id { get; set; } = 1;

    // Hero
    [Required, MaxLength(120)]
    public string FullName { get; set; } = "";

    [Required, MaxLength(160)]
    public string ProfessionalTitle { get; set; } = "";

    // Store a relative path like "/images/avatar.jpg"
    [MaxLength(250)]
    public string? PhotoUrl { get; set; }

    // About
    [Required, MaxLength(800)]
    public string AboutMe { get; set; } = "";

    // Contact (public-safe)
    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = "";

    [MaxLength(250)]
    public string? LinkedInUrl { get; set; }

    [MaxLength(250)]
    public string? GitHubUrl { get; set; }

    // City/Region only (no home address)
    [MaxLength(120)]
    public string? Region { get; set; }
}