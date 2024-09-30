using WebApiBasicAuth.Domain.Entities;

namespace WebApiBasicAuth.Services;

public interface IWeatherForecaster
{
    bool ForecastEnabled { get; }

    Task<WeatherResult> GetCurrentWeatherAsync(string city);
}
