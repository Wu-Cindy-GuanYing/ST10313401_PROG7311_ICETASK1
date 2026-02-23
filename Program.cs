using CV_Online_ST10313401.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineCv.Services;

namespace CV_Online_ST10313401
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? "Data Source=cv.db"));

            builder.Services
                .AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireDigit = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/denied";
            });

            builder.Services.AddSingleton<CvPdfService>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
                await SeedData.InitializeAsync(scope.ServiceProvider);
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Public CV
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Public}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
