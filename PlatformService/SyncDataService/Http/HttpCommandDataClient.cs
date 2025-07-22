using System.Text;
using System.Text.Json;
using PlatformService.Dto;

namespace PlatformService.SyncDataService.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;

    }
    public async Task SendPlatformToCommand(PlatformReadDto platformReadDto)
    {
        var httpContext = new StringContent(
           JsonSerializer.Serialize(platformReadDto),
              Encoding.UTF8,
                "application/json"

        );
        var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContext);
        // Check if the response is successful
        // and log the result
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Sync POST to Command Service was OK");
        }
        else
        {
            Console.WriteLine("--> Sync POST to Command Service was NOT OK");
        }
    }
}
