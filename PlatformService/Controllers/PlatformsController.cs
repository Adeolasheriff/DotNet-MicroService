using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Dto;
using PlatformService.Models;
using PlatformService.Service.Interface;
using PlatformService.SyncDataService.Http;

namespace PlatformService.Controllers;

[Route("api/platforms")]
[ApiController]
public class PlatformController : ControllerBase
{
    private readonly IPlatformRepo _platformRepo;
    private readonly IMapper _mapper;

    private readonly ICommandDataClient _commandDataClient;

    public PlatformController(
        IPlatformRepo platformRepo,
        ICommandDataClient commandDataClient,
         IMapper mapper

     )

    {
        _commandDataClient = commandDataClient;
        _platformRepo = platformRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
    {
        Console.WriteLine("--> Getting All Platforms");

        var platforms = _platformRepo.GetAllPlatforms();
        if (platforms == null || !platforms.Any())
        {
            return NotFound("No platforms found.");
        }

        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));

    }
    [HttpGet("{id}")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
        Console.WriteLine($"--> Getting Platform with ID: {id}");

        var platform = _platformRepo.GetPlatformById(id);
        if (platform == null)
        {
            return NotFound($"Platform with ID {id} not found.");
        }

        return Ok(_mapper.Map<PlatformReadDto>(platform));
    }
    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        Console.WriteLine("--> Creating New Platform");

        var platformModel = _mapper.Map<Platform>(platformCreateDto);
        _platformRepo.CreatePlatform(platformModel);
        _platformRepo.SaveChanges();

        var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
        // Send sync message
        try
        {
             await _commandDataClient.SendPlatformToCommand(platformReadDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
        }

        return Ok(platformReadDto);
    }
}
