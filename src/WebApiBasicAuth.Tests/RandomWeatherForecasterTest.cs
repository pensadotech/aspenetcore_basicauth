using WebApiBasicAuth.Domain.Entities;
using WebApiBasicAuth.Services;

namespace WebApiBasicAuth.Tests;

// This unit test exercie the RandomWeatherForecaster Service

public class RandomWeatherForecasterTest
{
    // Use Theory, feeding different data to the test to 
    // cover different scenarios
    [Theory]
    [Trait("Category", "WeatherComponents")]
    [InlineData("Irvine")]    
    [InlineData("Seattle")]
    public async Task MultipleCitiesGetCurrentWeatherAsync_ReturnsCurrentWeather_NotNull(string city)
    {
        // Arrange
        IWeatherForecaster weatherForcaster = new RandomWeatherForecaster();

        // Act
        WeatherResult? currentWeather = await weatherForcaster.GetCurrentWeatherAsync(city);


        // Asssert
        Assert.NotNull(currentWeather);
    }

    [Fact]
    [Trait("Category", "WeatherComponents")]
    public async Task DefaultGetCurrentWeatherAsync_ReturnsWeatheObject_NotNull()
    {
        // Arrange
        IWeatherForecaster weatherForcaster = new RandomWeatherForecaster();
        string city = "";


        // Act
        WeatherResult? currentWeather = await weatherForcaster.GetCurrentWeatherAsync(city);


        // Asssert
        Assert.NotNull(currentWeather.Weather);
    }

    [Fact]
    [Trait("Category", "WeatherComponents")]
    public async Task GetCurrentWeatherAsync_ReturnsWeatheObject_NotNull()
    {
        // Arrange
        IWeatherForecaster weatherForcaster = new RandomWeatherForecaster();
        string city = "Irvine";


        // Act
        WeatherResult? currentWeather = await weatherForcaster.GetCurrentWeatherAsync(city);


        // Asssert
        Assert.NotNull(currentWeather.Weather);
    }

    [Fact]
    [Trait("Category", "WeatherComponents")]
    public async Task GetCurrentWeatherAsync_ReturnsHasWeatherSummary_NotNull()
    {
        // Arrange
        IWeatherForecaster weatherForcaster = new RandomWeatherForecaster();
        string city = "Irvine";


        // Act
        WeatherResult? currentWeather = await weatherForcaster.GetCurrentWeatherAsync(city);


        // Asssert
        Assert.NotNull(currentWeather.Weather.Summary);
    }

    [Fact]
    [Trait("Category", "WeatherComponents")]
    public async Task GetCurrentWeatherAsync_ReturnsWeatherWind_NotNull()
    {
        // Arrange
        IWeatherForecaster weatherForcaster = new RandomWeatherForecaster();
        string city = "Irvine";


        // Act
        WeatherResult? currentWeather = await weatherForcaster.GetCurrentWeatherAsync(city);


        // Asssert
        Assert.NotNull(currentWeather.Weather.Wind);
    }

    [Fact]
    [Trait("Category", "WeatherComponents")]
    public async Task GetCurrentWeatherAsync_ReturnsWeatherTemperature_NotNull()
    {
        // Arrange
        IWeatherForecaster weatherForcaster = new RandomWeatherForecaster();
        string city = "Irvine";


        // Act
        WeatherResult? currentWeather = await weatherForcaster.GetCurrentWeatherAsync(city);


        // Asssert
        Assert.NotNull(currentWeather.Weather.Temperature);
    }

    [Fact]
    [Trait("Category", "WeatherDetails")]
    public async Task GetCurrentWeatherAsync_ReturnsWeatherWindSpeedDegrees_notNull()
    {
        // Arrange
        IWeatherForecaster weatherForcaster = new RandomWeatherForecaster();
        string city = "Irvine";


        // Act
        WeatherResult? currentWeather = await weatherForcaster.GetCurrentWeatherAsync(city);


        // Asssert
        Assert.True(currentWeather.Weather.Wind.Speed != null && currentWeather.Weather.Wind.Degrees != 0);
    }

    [Fact]
    [Trait("Category", "WeatherDetails")]
    public async Task GetCurrentWeatherAsync_ReturnsWeatherTemperatureMinMax_NotNull()
    {
        // Arrange
        IWeatherForecaster weatherForcaster = new RandomWeatherForecaster();
        string city = "Irvine";


        // Act
        WeatherResult? currentWeather = await weatherForcaster.GetCurrentWeatherAsync(city);


        // Asssert
        Assert.True(currentWeather.Weather.Temperature.Min != null && currentWeather.Weather.Temperature.Max != null);
    }
}
