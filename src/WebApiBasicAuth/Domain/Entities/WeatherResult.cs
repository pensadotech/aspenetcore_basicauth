namespace WebApiBasicAuth.Domain.Entities;

public class WeatherResult
{
    public string City { get; init; }

    public string ExecutionDateTime { get; set; }

    public WeatherCondition? Weather { get; init; } = new WeatherCondition();
}
