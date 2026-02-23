using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineCv.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    public IActionResult Index() => View();
}