namespace CV_Online_ST10313401.Models;

public class PublicCvViewModel
{
    public CvProfile Profile { get; set; } = new();
    public List<SkillGroup> Skills { get; set; } = [];
    public List<ProjectItem> Projects { get; set; } = [];
    public List<EducationItem> Education { get; set; } = [];
    public List<ExperienceItem> Experience { get; set; } = [];
    public List<CertificationItem> Certifications { get; set; } = [];
    public List<VolunteerItem> Volunteering { get; set; } = [];
    public List<InterestItem> Interests { get; set; } = [];
}