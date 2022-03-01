namespace InterviewAssessment.Services.Implementations;

using System.Text.Json;
using InterviewAssessment.Models;
using InterviewAssessment.Models.Settings;
using InterviewAssessment.Services.Interfaces;
using Microsoft.Extensions.Options;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<WeatherSettings> _settings;

    public WeatherService(HttpClient httpClient, IOptions<WeatherSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
    }

    /// <inheritdoc />
    public async Task<WeatherResponse> GetWeatherByZipCodeAsync(int zip, CancellationToken cancel = default)
    {
        var url = $"{_settings.Value.BaseUrl}/query?zip={zip}";
        var response = await _httpClient.GetStringAsync(url, cancel);
        var data = JsonSerializer.Deserialize<WeatherResponse>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return data;
    }
}
