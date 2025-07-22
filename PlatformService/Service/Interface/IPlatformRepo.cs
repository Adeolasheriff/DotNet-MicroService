using PlatformService.Models;

namespace PlatformService.Service.Interface;

public interface IPlatformRepo
{
    bool SaveChanges();
    IEnumerable<Platform> GetAllPlatforms();

    Platform GetPlatformById(int id);

    void CreatePlatform(Platform platform);
}
