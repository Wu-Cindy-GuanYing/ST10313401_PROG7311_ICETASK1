using System.ComponentModel.DataAnnotations;

namespace OnlineCv.Models;

public class SkillGroup
{
    public int Id { get; set; }

    [Required, MaxLength(60)]
    public string Title { get; set; } = "Languages";

    // Store as comma-separated tags for simplicity
    [Required, MaxLength(800)]
    public string TagsCsv { get; set; } = "C#, Java, Python";
}