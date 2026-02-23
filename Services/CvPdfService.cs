using OnlineCv.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace OnlineCv.Services;

public class CvPdfService
{
    public byte[] Generate(PublicCvViewModel vm)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var doc = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Content().Column(col =>
                {
                    col.Item().Text(vm.Profile.FullName).FontSize(20).SemiBold();
                    col.Item().Text(vm.Profile.ProfessionalTitle).FontSize(12);
                    col.Item().Text($"{vm.Profile.Email} | {vm.Profile.LinkedInUrl} | {vm.Profile.GitHubUrl}");

                    if (!string.IsNullOrWhiteSpace(vm.Profile.AboutMe))
                    {
                        col.Item().PaddingTop(10).Text("About Me").SemiBold();
                        col.Item().Text(vm.Profile.AboutMe);
                    }

                    if (vm.Skills.Any())
                    {
                        col.Item().PaddingTop(10).Text("Skills").SemiBold();
                        foreach (var g in vm.Skills)
                            col.Item().Text($"{g.Title}: {g.TagsCsv}");
                    }

                    if (vm.Projects.Any())
                    {
                        col.Item().PaddingTop(10).Text("Projects").SemiBold();
                        foreach (var p in vm.Projects)
                        {
                            col.Item().Text(p.Name).SemiBold();
                            col.Item().Text(p.Description);
                            col.Item().Text($"Tech: {p.TechStack}");
                            col.Item().Text($"Repo: {p.GitHubUrl}");
                            if (!string.IsNullOrWhiteSpace(p.LiveDemoUrl))
                                col.Item().Text($"Demo: {p.LiveDemoUrl}");
                            col.Item().PaddingBottom(6);
                        }
                    }

                    if (vm.Education.Any())
                    {
                        col.Item().PaddingTop(10).Text("Education").SemiBold();
                        foreach (var e in vm.Education)
                        {
                            col.Item().Text($"{e.Degree} — {e.Institution} ({e.ExpectedGraduation})").SemiBold();
                            if (!string.IsNullOrWhiteSpace(e.Highlights))
                                col.Item().Text(e.Highlights);
                        }
                    }

                    if (vm.Experience.Any())
                    {
                        col.Item().PaddingTop(10).Text("Experience").SemiBold();
                        foreach (var x in vm.Experience)
                        {
                            col.Item().Text($"{x.Role} — {x.Company} ({x.DateRange})").SemiBold();
                            if (!string.IsNullOrWhiteSpace(x.Bullets))
                                col.Item().Text(x.Bullets.Replace("\n", " • "));
                        }
                    }

                    if (vm.Certifications.Any())
                    {
                        col.Item().PaddingTop(10).Text("Certifications").SemiBold();
                        foreach (var c in vm.Certifications)
                            col.Item().Text($"{c.Name} — {c.Issuer} ({c.Year}) {c.Url}");
                    }

                    if (vm.Volunteering.Any())
                    {
                        col.Item().PaddingTop(10).Text("Volunteering / Leadership").SemiBold();
                        foreach (var v in vm.Volunteering)
                            col.Item().Text($"{v.Title} — {v.Org}: {v.Description}");
                    }

                    if (vm.Interests.Any())
                    {
                        col.Item().PaddingTop(10).Text("Interests").SemiBold();
                        foreach (var i in vm.Interests)
                            col.Item().Text(i.Text);
                    }
                });
            });
        });

        return doc.GeneratePdf();
    }
}