using System.ComponentModel.DataAnnotations;

namespace CV_Online_ST10313401.Models;

public class InterestItem
{
    public int Id { get; set; }

    [Required, MaxLength(140)]
    public string Text { get; set; } = "Chess enthusiast and amateur photographer.";
}