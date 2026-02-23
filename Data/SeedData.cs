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

        var adminEmail = "st10313401@myemeris.edu.za";
        var adminPassword = "ST10313401@";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var created = await userManager.CreateAsync(adminUser, adminPassword);
            if (created.Succeeded)
                await userManager.AddToRoleAsync(adminUser, adminRole);
        }

        if (!await db.Profiles.AnyAsync())
        {
            db.Profiles.Add(new CvProfile
            {
                Id = 1,
                FullName = "Cindy Wu",
                ProfessionalTitle = "Final-Year IT Student · Aspiring Software Developer",
                AboutMe = "I’m a final-year IT student focused on building reliable software...",
                Email = "st10313401@myemeris.edu.za",
                LinkedInUrl = "https://www.linkedin.com/in/yourprofile",
                GitHubUrl = "https://github.com/yourname",
                Region = "Johannesburg, South Africa"
            });

            await db.SaveChangesAsync();
        }
    }
}