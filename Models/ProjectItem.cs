using System.ComponentModel.DataAnnotations;

namespace CV_Online_ST10313401.Models;

public class ProjectItem
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Name { get; set; } = "";

    [Required, MaxLength(500)]
    public string Description { get; set; } = "";

    [Required, MaxLength(200)]
    public string TechStack { get; set; } = "ASP.NET Core, EF Core, SQLite";

    [Required, MaxLength(250)]
    public string GitHubUrl { get; set; } = "";

    [MaxLength(250)]
    public string? LiveDemoUrl { get; set; }

    // Optional screenshot path
    [MaxLength(250)]
    public string? ScreenshotUrl { get; set; }
}