using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CV_Online_ST10313401.Models;

namespace CV_Online_ST10313401.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        await db.Database.MigrateAsync();

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        const string adminRole = "Admin";
        if (!await roleManager.RoleExistsAsync(adminRole))
            await roleManager.CreateAsync(new IdentityRole(adminRole));

        var adminEmail = "admin@yourcv.com";
        var adminPassword = "ChangeMe123!";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var created = await userManager.CreateAsync(adminUser, adminPassword);
            if (created.Succeeded)
                await userManager.AddToRoleAsync(adminUser, adminRole);
        }

        // Create a default CV profile (single-profile app)
        if (!await db.Profiles.AnyAsync())
        {
            db.Profiles.Add(new CvProfile
            {
                FullName = "Your Name",
                ProfessionalTitle = "Final-Year IT Student · Aspiring Software Developer",
                AboutMe = "I’m a final-year IT student focused on building practical software. I enjoy backend development and clean UI. I’m passionate about learning and shipping useful tools.",
                Email = "you@email.com",
                LinkedInUrl = "https://www.linkedin.com/in/yourprofile",
                GitHubUrl = "https://github.com/yourname",
                Region = "South Africa"
            });
            await db.SaveChangesAsync();
        }
    }
}