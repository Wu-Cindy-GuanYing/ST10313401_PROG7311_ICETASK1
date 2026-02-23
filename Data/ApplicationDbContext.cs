using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineCv.Models;

namespace OnlineCv.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<CvProfile> Profiles => Set<CvProfile>();
    public DbSet<SkillGroup> SkillGroups => Set<SkillGroup>();
    public DbSet<ProjectItem> Projects => Set<ProjectItem>();
    public DbSet<EducationItem> Education => Set<EducationItem>();
    public DbSet<ExperienceItem> Experience => Set<ExperienceItem>();
    public DbSet<CertificationItem> Certifications => Set<CertificationItem>();
    public DbSet<VolunteerItem> Volunteering => Set<VolunteerItem>();
    public DbSet<InterestItem> Interests => Set<InterestItem>();
}