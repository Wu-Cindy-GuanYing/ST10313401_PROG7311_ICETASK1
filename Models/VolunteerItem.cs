using System.ComponentModel.DataAnnotations;

namespace OnlineCv.Models;

public class VolunteerItem
{
    public int Id { get; set; }

    [Required, MaxLength(160)]
    public string Title { get; set; } = "";

    [MaxLength(120)]
    public string? Org { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }
}