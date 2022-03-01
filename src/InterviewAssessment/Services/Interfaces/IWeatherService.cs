namespace InterviewAssessment.Services.Interfaces;

using InterviewAssessment.Models;

public interface IWeatherService
{
    Task<WeatherResponse> GetWeatherByZipCodeAsync(int zip, CancellationToken cancel = default);
}
