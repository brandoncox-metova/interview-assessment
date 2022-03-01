namespace InterviewAssessment.Controllers;

using InterviewAssessment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly IWeatherService _weatherService;

    /// <inheritdoc />
    public HomeController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public IActionResult Index()
    {
        return NoContent();
    }

    [HttpGet("weather")]
    public async Task<IActionResult> FetchWeather(int zip, CancellationToken cancel = default)
    {
        var result = await _weatherService.GetWeatherByZipCodeAsync(zip, cancel);
        return Ok(result);
    }
}
