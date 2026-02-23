using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CV_Online_ST10313401.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    public IActionResult Index() => View();
}