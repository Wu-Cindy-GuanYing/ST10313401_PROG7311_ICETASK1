using System.ComponentModel.DataAnnotations;

namespace OnlineCv.Models;

public class CertificationItem
{
    public int Id { get; set; }

    [Required, MaxLength(160)]
    public string Name { get; set; } = "";

    [MaxLength(120)]
    public string? Issuer { get; set; }

    [MaxLength(40)]
    public string? Year { get; set; }

    [MaxLength(250)]
    public string? Url { get; set; }
}