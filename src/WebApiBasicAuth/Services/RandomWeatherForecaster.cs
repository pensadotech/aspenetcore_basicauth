using WebApiBasicAuth.Domain.Entities;

namespace WebApiBasicAuth.Services;

public class RandomWeatherForecaster : IWeatherForecaster
{
    private readonly Random _random = new();
    public bool ForecastEnabled => true;

    public Task<WeatherResult> GetCurrentWeatherAsync(string city)
    {
        // Generate a reandom number, from 1 to 4
        var condition = _random.Next(1, 4);

        // Select one of the four random conditions based on the random number
        // using the .NET 8 new syntax
        var currentWeather = condition switch
        {
            1 => new WeatherResult
            {
                City = city,
                Weather = new WeatherCondition
                {
                    Summary = "Sun",
                    Temperature = new Temperature(26, 32),
                    Wind = new Wind(6, 190)
                }
            },
            2 => new WeatherResult
            {
                City = city,
                Weather = new WeatherCondition
                {
                    Summary = "Rain",
                    Temperature = new Temperature(8, 14),
                    Wind = new Wind(3, 80)
                }
            },
            3 => new WeatherResult
            {
                City = city,
                Weather = new WeatherCondition
                {
                    Summary = "Cloud",
                    Temperature = new Temperature(12, 18),
                    Wind = new Wind(1, 10)
                }
            },
            _ => new WeatherResult
            {
                City = city,
                Weather = new WeatherCondition
                {
                    Summary = "Snow",
                    Temperature = new Temperature(-2, 1),
                    Wind = new Wind(8, 240)
                }
            },
        };

        // Stamp curent deate and time
        currentWeather.ExecutionDateTime = DateTime.Now.ToString();

        return Task.FromResult(currentWeather);
    }

}
