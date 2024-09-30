using k8s;
using Microsoft.AspNetCore.Routing.Constraints;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBasicAuth.Domain.Entities;
using WebApiBasicAuth.Tests.Fixtures;
using Xunit.Abstractions;

namespace WebApiBasicAuth.Tests;

// Because the domain classes have getter and setter that are straight forward
// the exercise can be only to test teh constructors. However, if that was not 
// the case, it will be necesary to test getter/setters also.

public class DomainTest 
{
    [Fact]
    public void Temperature_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        float expectedMin = -10.5f;
        float expectedMax = 35.0f;

        // Act
        var temperature = new Temperature(expectedMin, expectedMax);

        // Assert
        Assert.Equal(expectedMin, temperature.Min);
        Assert.Equal(expectedMax, temperature.Max);

    }

    [Fact]
    public void Wind_Constructor_ShouldIitializeProperties()
    {
        // Arrange
        float expectedSpeed = 10.5f;
        float expectedDegrees = 35.0f;

        // Act
        var wind = new Wind(expectedSpeed, expectedDegrees);

        // Assert
        Assert.Equal(expectedSpeed, wind.Speed);
        Assert.Equal(expectedDegrees, wind.Degrees);
    }
    
    [Fact]
    public void WeatherConditions_Constructor_ShouldInitializeProperties()
    {
        // Arrange 
        var expectedSummary = "Unknown";
        var expectedWind = new Wind(0,0);
        var expectedTemperature = new Temperature(0, 0);

        // Act
        var weatherConditions = new WeatherCondition();

        // Assert
        Assert.Equal(expectedSummary, weatherConditions.Summary);
        Assert.Equal(expectedWind.Speed, weatherConditions.Wind.Speed);
        Assert.Equal(expectedWind.Degrees, weatherConditions.Wind.Degrees);
        Assert.Equal(expectedTemperature.Min, weatherConditions.Temperature.Min);
        Assert.Equal(expectedTemperature.Max, weatherConditions.Temperature.Max);
    }

    [Fact]
    public void WeatherResult_Constructor_ShouldInitializeProperties()
    {
        // Act 
        var weatherResult = new WeatherResult();

        // Assert
        Assert.Null(weatherResult.City);
        Assert.Null(weatherResult.ExecutionDateTime);
        Assert.NotNull(weatherResult.Weather);
        Assert.Equal(0, weatherResult.Weather.Wind.Speed);
        Assert.Equal(0, weatherResult.Weather.Wind.Degrees);
        Assert.Equal(0, weatherResult.Weather.Temperature.Min);
        Assert.Equal(0, weatherResult.Weather.Temperature.Max);
    }

    [Fact]
    public void WeatherConditions_Properties_SHouldSetAndGetValues()
    {
        // Arrange
        var city = "New York";
        var executionDateTime = "2024-10-01T12:00:00Z";
        var weather = new WeatherCondition
        {
            Summary = "Sunny",
            Wind = new Wind(10, 180),
            Temperature = new Temperature(15, 25)
        };

        // Act
        var weatherResult = new WeatherResult
        {
            City = city,
            ExecutionDateTime = executionDateTime,
            Weather = weather
        };

        // Assert
        Assert.Equal(city, weatherResult.City);
        Assert.Equal(executionDateTime, weatherResult.ExecutionDateTime);
        Assert.Equal(weather, weatherResult.Weather);

    }

}
