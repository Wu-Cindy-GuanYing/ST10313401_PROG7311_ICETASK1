using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CV_Online_ST10313401.Data;
using CV_Online_ST10313401.Models;

namespace CV_Online_ST10313401.Controllers;

[Authorize(Roles = "Admin")]
public class ManageController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _env;

    public ManageController(ApplicationDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    // ----------------------
    // PROFILE
    // ----------------------
    public async Task<IActionResult> Profile()
    {
        var p = await _db.Profiles.FirstAsync(x => x.Id == 1);
        return View(p);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Profile(CvProfile model)
    {
        if (!ModelState.IsValid) return View(model);

        var p = await _db.Profiles.FirstAsync(x => x.Id == 1);
        p.FullName = model.FullName;
        p.ProfessionalTitle = model.ProfessionalTitle;
        p.PhotoUrl = model.PhotoUrl;
        p.AboutMe = model.AboutMe;
        p.Email = model.Email;
        p.LinkedInUrl = model.LinkedInUrl;
        p.GitHubUrl = model.GitHubUrl;
        p.Region = model.Region;

        await _db.SaveChangesAsync();
        TempData["ok"] = "Profile updated.";
        return RedirectToAction(nameof(Profile));
    }

    // Optional: upload avatar -> saves to /uploads and sets PhotoUrl
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadAvatar(IFormFile avatar)
    {
        if (avatar == null || avatar.Length == 0)
        {
            TempData["err"] = "Please choose an image file.";
            return RedirectToAction(nameof(Profile));
        }

        // Basic validation
        var allowed = new[] { "image/jpeg", "image/png", "image/webp" };
        if (!allowed.Contains(avatar.ContentType))
        {
            TempData["err"] = "Only JPG, PNG, or WebP images are allowed.";
            return RedirectToAction(nameof(Profile));
        }

        if (avatar.Length > 2 * 1024 * 1024)
        {
            TempData["err"] = "Max file size is 2MB.";
            return RedirectToAction(nameof(Profile));
        }

        var uploadsDir = Path.Combine(_env.WebRootPath, "uploads");
        Directory.CreateDirectory(uploadsDir);

        var ext = Path.GetExtension(avatar.FileName);
        var fileName = $"avatar_{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid():N}{ext}";
        var filePath = Path.Combine(uploadsDir, fileName);

        await using (var stream = System.IO.File.Create(filePath))
        {
            await avatar.CopyToAsync(stream);
        }

        var p = await _db.Profiles.FirstAsync(x => x.Id == 1);
        p.PhotoUrl = $"/uploads/{fileName}";
        await _db.SaveChangesAsync();

        TempData["ok"] = "Avatar uploaded.";
        return RedirectToAction(nameof(Profile));
    }

    // ----------------------
    // SKILLS
    // ----------------------
    public async Task<IActionResult> Skills()
        => View(await _db.SkillGroups.OrderBy(x => x.Id).ToListAsync());

    public IActionResult AddSkill() => View(new SkillGroup());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddSkill(SkillGroup model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.SkillGroups.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Skills));
    }

    public async Task<IActionResult> EditSkill(int id)
    {
        var item = await _db.SkillGroups.FindAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditSkill(SkillGroup model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.SkillGroups.Update(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Skills));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteSkill(int id)
    {
        var item = await _db.SkillGroups.FindAsync(id);
        if (item != null)
        {
            _db.SkillGroups.Remove(item);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Skills));
    }

    // ----------------------
    // PROJECTS
    // ----------------------
    public async Task<IActionResult> Projects()
        => View(await _db.Projects.OrderByDescending(x => x.Id).ToListAsync());

    public IActionResult AddProject() => View(new ProjectItem());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddProject(ProjectItem model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Projects.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Projects));
    }

    public async Task<IActionResult> EditProject(int id)
    {
        var item = await _db.Projects.FindAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProject(ProjectItem model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Projects.Update(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Projects));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var item = await _db.Projects.FindAsync(id);
        if (item != null)
        {
            _db.Projects.Remove(item);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Projects));
    }

    // ----------------------
    // EDUCATION
    // ----------------------
    public async Task<IActionResult> Education()
        => View(await _db.Education.OrderByDescending(x => x.Id).ToListAsync());

    public IActionResult AddEducation() => View(new EducationItem());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddEducation(EducationItem model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Education.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Education));
    }

    public async Task<IActionResult> EditEducation(int id)
    {
        var item = await _db.Education.FindAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditEducation(EducationItem model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Education.Update(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Education));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteEducation(int id)
    {
        var item = await _db.Education.FindAsync(id);
        if (item != null)
        {
            _db.Education.Remove(item);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Education));
    }

    // ----------------------
    // EXPERIENCE
    // ----------------------
    public async Task<IActionResult> Experience()
        => View(await _db.Experience.OrderByDescending(x => x.Id).ToListAsync());

    public IActionResult AddExperience() => View(new ExperienceItem());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddExperience(ExperienceItem model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Experience.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Experience));
    }

    public async Task<IActionResult> EditExperience(int id)
    {
        var item = await _db.Experience.FindAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditExperience(ExperienceItem model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Experience.Update(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Experience));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteExperience(int id)
    {
        var item = await _db.Experience.FindAsync(id);
        if (item != null)
        {
            _db.Experience.Remove(item);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Experience));
    }

    // ----------------------
    // CERTIFICATIONS
    // ----------------------
    public async Task<IActionResult> Certifications()
        => View(await _db.Certifications.OrderByDescending(x => x.Id).ToListAsync());

    public IActionResult AddCertification() => View(new CertificationItem());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCertification(CertificationItem model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Certifications.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Certifications));
    }

    public async Task<IActionResult> EditCertification(int id)
    {
        var item = await _db.Certifications.FindAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCertification(CertificationItem model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Certifications.Update(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Certifications));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCertification(int id)
    {
        var item = await _db.Certifications.FindAsync(id);
        if (item != null)
        {
            _db.Certifications.Remove(item);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Certifications));
    }

    // ----------------------
    // VOLUNTEERING / LEADERSHIP
    // ----------------------
    public async Task<IActionResult> Volunteering()
        => View(await _db.Volunteering.OrderByDescending(x => x.Id).ToListAsync());

    public IActionResult AddVolunteer() => View(new VolunteerItem());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddVolunteer(VolunteerItem model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Volunteering.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Volunteering));
    }

    public async Task<IActionResult> EditVolunteer(int id)
    {
        var item = await _db.Volunteering.FindAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditVolunteer(VolunteerItem model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Volunteering.Update(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Volunteering));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteVolunteer(int id)
    {
        var item = await _db.Volunteering.FindAsync(id);
        if (item != null)
        {
            _db.Volunteering.Remove(item);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Volunteering));
    }

    // ----------------------
    // INTERESTS
    // ----------------------
    public async Task<IActionResult> Interests()
        => View(await _db.Interests.OrderByDescending(x => x.Id).ToListAsync());

    public IActionResult AddInterest() => View(new InterestItem());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddInterest(InterestItem model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Interests.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Interests));
    }

    public async Task<IActionResult> EditInterest(int id)
    {
        var item = await _db.Interests.FindAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditInterest(InterestItem model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Interests.Update(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Interests));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteInterest(int id)
    {
        var item = await _db.Interests.FindAsync(id);
        if (item != null)
        {
            _db.Interests.Remove(item);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Interests));
    }
}