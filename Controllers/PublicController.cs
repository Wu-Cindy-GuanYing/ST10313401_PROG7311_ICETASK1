using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCv.Data;
using OnlineCv.Models;
using OnlineCv.Services;

namespace OnlineCv.Controllers;

public class PublicController : Controller
{
    private readonly ApplicationDbContext _db;

    public PublicController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var profile = await _db.Profiles.FirstAsync(p => p.Id == 1);

        var vm = new PublicCvViewModel
        {
            Profile = profile,
            Skills = await _db.SkillGroups.OrderBy(s => s.Id).ToListAsync(),
            Projects = await _db.Projects.OrderByDescending(p => p.Id).ToListAsync(),
            Education = await _db.Education.OrderByDescending(e => e.Id).ToListAsync(),
            Experience = await _db.Experience.OrderByDescending(x => x.Id).ToListAsync(),
            Certifications = await _db.Certifications.OrderByDescending(c => c.Id).ToListAsync(),
            Volunteering = await _db.Volunteering.OrderByDescending(v => v.Id).ToListAsync(),
            Interests = await _db.Interests.OrderByDescending(i => i.Id).ToListAsync()
        };

        return View(vm);
    }

    [HttpGet("/cv.pdf")]
    public async Task<IActionResult> Pdf([FromServices] CvPdfService pdf)
    {
        var profile = await _db.Profiles.FirstAsync(p => p.Id == 1);

        var vm = new PublicCvViewModel
        {
            Profile = profile,
            Skills = await _db.SkillGroups.ToListAsync(),
            Projects = await _db.Projects.ToListAsync(),
            Education = await _db.Education.ToListAsync(),
            Experience = await _db.Experience.ToListAsync(),
            Certifications = await _db.Certifications.ToListAsync(),
            Volunteering = await _db.Volunteering.ToListAsync(),
            Interests = await _db.Interests.ToListAsync()
        };

        var bytes = pdf.Generate(vm);
        return File(bytes, "application/pdf", $"{profile.FullName}-CV.pdf");
    }
}