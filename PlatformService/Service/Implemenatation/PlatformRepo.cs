using PlatformService.Data;
using PlatformService.Models;
using PlatformService.Service.Interface;

namespace PlatformService.Service.Implemenatation;

public class PlatformRepo : IPlatformRepo
{
    private readonly AppDbContext _context;
    public PlatformRepo(AppDbContext context)
    {
        _context = context;
    }
    public void CreatePlatform(Platform platform)
    {
        if (platform == null)
        {
            throw new ArgumentNullException(nameof(platform));
        }
        _context.Platforms.Add(platform);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        var result = _context.Platforms.ToList();
        Console.WriteLine($"📦 Repo: Retrieved {result.Count} platforms from DB");
        return result;
    }

    public Platform GetPlatformById(int id)
    {
        return _context.Platforms.FirstOrDefault(p => p.Id == id);
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }
}