using System.ComponentModel.DataAnnotations;

namespace CV_Online_ST10313401.Models;

public class ExperienceItem
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Role { get; set; } = "";

    [Required, MaxLength(120)]
    public string Company { get; set; } = "";

    [MaxLength(80)]
    public string? DateRange { get; set; } // "Jun 2024 – Present"

    [MaxLength(700)]
    public string? Bullets { get; set; } // store as "- did x\n- did y"
}