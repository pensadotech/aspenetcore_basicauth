using WebApiBasicAuth.Domain.Entities;

namespace WebApiBasicAuth.Services;

// INterface for weather services
public interface IWeatherForecaster
{
    bool ForecastEnabled { get; }

    Task<WeatherResult> GetCurrentWeatherAsync(string city);
}
