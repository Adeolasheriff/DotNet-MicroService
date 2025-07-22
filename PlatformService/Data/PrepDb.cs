using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder applicationBuilder,bool IsProduction)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), IsProduction);
        }
    }
    private static void SeedData(AppDbContext context,bool IsProduction)
    {
        if (IsProduction)
        {
            Console.WriteLine("--> Applying Migrations...");
            try
            {
                context.Database.Migrate();
                Console.WriteLine("✅ Migrations applied successfully");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            }
        }

        if (!context.Platforms.Any())
        {
            Console.WriteLine("--> Seeding Data...");

            context.Platforms.AddRange(
                new Platform() { Name = "DotNet", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
            );
            Console.WriteLine("✅ Controller action reached");


            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");

        }

    }
}
