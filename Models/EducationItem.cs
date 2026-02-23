using System.ComponentModel.DataAnnotations;

namespace OnlineCv.Models;

public class EducationItem
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Degree { get; set; } = "";

    [Required, MaxLength(120)]
    public string Institution { get; set; } = "";

    [MaxLength(60)]
    public string? ExpectedGraduation { get; set; } // e.g. "2026"

    [MaxLength(500)]
    public string? Highlights { get; set; } // modules, achievements
}