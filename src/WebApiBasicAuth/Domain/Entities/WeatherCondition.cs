namespace WebApiBasicAuth.Domain.Entities;

// Entity to store Weather conditions
public class WeatherCondition
{
    public string Summary { get; init; } = "Unknown";
    public Wind Wind { get; init; } = new Wind(0, 0);
    public Temperature Temperature { get; init; } = new Temperature(0, 0);
   
}
