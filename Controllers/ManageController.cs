using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCv.Data;
using OnlineCv.Models;

namespace OnlineCv.Controllers;

[Authorize(Roles = "Admin")]
public class ManageController : Controller
{
    private readonly ApplicationDbContext _db;
    public ManageController(ApplicationDbContext db) => _db = db;

    // --- Profile ---
    public async Task<IActionResult> Profile()
    {
        var p = await _db.Profiles.FirstAsync(x => x.Id == 1);
        return View(p);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
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

    // SKILLS
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

    // --- Projects ---
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
}