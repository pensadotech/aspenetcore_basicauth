using WebApiBasicAuth.Services;

namespace WebApiBasicAuth.ServiceCollectionsExtensions;

public static class WeatherCollectionExtensions
{
    public static IServiceCollection AddWeatherServices(this IServiceCollection services,
        IConfiguration configuration)
    {

        // All all weatehr extensions
        services.AddSingleton<IWeatherForecaster, RandomWeatherForecaster>();


        return services;
    }
}
