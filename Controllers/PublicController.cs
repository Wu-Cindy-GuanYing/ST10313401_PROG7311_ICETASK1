using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CV_Online_ST10313401.Data;
using CV_Online_ST10313401.Models;

namespace CV_Online_ST10313401.Controllers;

public class PublicController : Controller
{
    private readonly ApplicationDbContext _db;
    public PublicController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        // If profile doesn't exist yet, create it once (so site never breaks)
        var profile = await _db.Profiles.FirstOrDefaultAsync(p => p.Id == 1);
        if (profile == null)
        {
            profile = new CvProfile
            {
                Id = 1,
                FullName = "Your Name",
                ProfessionalTitle = "Final-Year IT Student · Aspiring Software Developer",
                AboutMe = "Write 2–3 sentences about you here.",
                Email = "you@email.com",
                LinkedInUrl = "https://www.linkedin.com/in/yourprofile",
                GitHubUrl = "https://github.com/yourname",
                Region = "South Africa"
            };
            _db.Profiles.Add(profile);
            await _db.SaveChangesAsync();
        }

        var vm = new PublicCvViewModel
        {
            Profile = profile,
            Skills = await _db.SkillGroups.OrderBy(x => x.Id).ToListAsync(),
            Projects = await _db.Projects.OrderByDescending(x => x.Id).ToListAsync(),
            Education = await _db.Education.OrderByDescending(x => x.Id).ToListAsync(),
            Experience = await _db.Experience.OrderByDescending(x => x.Id).ToListAsync(),
            Certifications = await _db.Certifications.OrderByDescending(x => x.Id).ToListAsync(),
            Volunteering = await _db.Volunteering.OrderByDescending(x => x.Id).ToListAsync(),
            Interests = await _db.Interests.OrderByDescending(x => x.Id).ToListAsync()
        };

        return View(vm);
    }
}