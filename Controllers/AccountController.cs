using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CV_Online_ST10313401.Models;

namespace CV_Online_ST10313401.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(SignInManager<IdentityUser> signInManager)
        => _signInManager = signInManager;

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
        => View(new LoginViewModel { ReturnUrl = returnUrl });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
        if (result.Succeeded)
            return LocalRedirect(model.ReturnUrl ?? "/admin");

        ModelState.AddModelError("", "Invalid login.");
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Public");
    }

    public IActionResult Denied() => View();
}