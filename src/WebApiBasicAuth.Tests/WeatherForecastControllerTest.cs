using Castle.Core.Logging;
using Google.Protobuf.WellKnownTypes;
using IdentityModel.OidcClient;
using k8s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBasicAuth.Controllers;
using WebApiBasicAuth.Domain.Entities;
using WebApiBasicAuth.Domain.Models;
using WebApiBasicAuth.Services;
using WebApiBasicAuth.Tests.BasicAuthHandlers;


namespace WebApiBasicAuth.Tests;

// This unit test exercise the WeatherForecastController GET methods
// for teh CurrentWeather(defualt city) and Weather for a selected city.

public class WeatherForecastControllerTest
{
    private readonly Mock<ILogger<WeatherForecastController>> _loggerMock;
    private readonly string _currentCity;
    private readonly string targetCity;

    public WeatherForecastControllerTest()
    {
        // Define commone elements for all test, but this simple approach
        // come with a price, as the constructore will be hit before starting 
        // each test. In this case are simple element. For moe complex 
        // situations, it is recommend to use a test Fixture, in which the
        // initialized element will be hot only once. 
        _loggerMock = new Mock<ILogger<WeatherForecastController>>();
        _currentCity = "Current City";
        targetCity = "Irvine";
    }

    [Fact]
    public async Task GetCurrentWeather_GetAction_MustReturnOkObjectResult()
    {
        // Arrange

        // Create a service mock 
        var weatherForecastServiceMock = new Mock<IWeatherForecaster>();
        weatherForecastServiceMock
            .Setup(m => m.GetCurrentWeatherAsync(_currentCity))
            .ReturnsAsync(new WeatherResult()
            {
                City = _currentCity,
                ExecutionDateTime = DateTime.Now.ToString(),
                Weather = new WeatherCondition()
            });

        // Instantiate he controller injecting necesary parameters
        var weatherForecastController = new WeatherForecastController(weatherForecastServiceMock.Object,
                                                                   _loggerMock.Object);

        // Act
        var result = await weatherForecastController.GetCurrentWeatherForecast();

        // Assert

        // Get acton result
        var actionResult = Assert.IsType<ActionResult<WeatherResult>>(result);
        // Evaluate that the type of respond is "Ok"
        Assert.IsType<OkObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetCurrentWeather_GetAction_MustReturWeatherResult()
    {
        // Arrange

        // Create a service mock 
        var weatherForecastServiceMock = new Mock<IWeatherForecaster>();
        weatherForecastServiceMock
            .Setup(m => m.GetCurrentWeatherAsync(_currentCity))
            .ReturnsAsync(new WeatherResult()
            {
                City = _currentCity,
                ExecutionDateTime = DateTime.Now.ToString(),
                Weather = new WeatherCondition()
            });

        // Instantiate he controller injecting necesary parameters
        var weatherForecastController = new WeatherForecastController(weatherForecastServiceMock.Object,
                                                                   _loggerMock.Object);

        // Act
        var result = await weatherForecastController.GetCurrentWeatherForecast();

        // Assert

        // Get acton result
        var actionResult = Assert.IsType<ActionResult<WeatherResult>>(result);
        // This evaluate the response to be type "WeatherResult"
        Assert.IsAssignableFrom<WeatherResult>(
                ((OkObjectResult)actionResult.Result).Value);

    }

    [Fact]
    public async Task GetCurrentWeather_GetAction_MustReturWeatherResultWithValidProperties()
    {
        // Arrange

        // Create a service mock 
        var weatherForecastServiceMock = new Mock<IWeatherForecaster>();
        weatherForecastServiceMock
            .Setup(m => m.GetCurrentWeatherAsync(_currentCity))
            .ReturnsAsync(new WeatherResult()
            {
                City = _currentCity,
                ExecutionDateTime = DateTime.Now.ToString(),
                Weather = new WeatherCondition()
            });

        // Instantiate he controller injecting necesary parameters
        var weatherForecastController = new WeatherForecastController(weatherForecastServiceMock.Object,
                                                                   _loggerMock.Object);

        // Act
        var result = await weatherForecastController.GetCurrentWeatherForecast();

        // Assert

        // Better organized way to assert a result object type value
        var actionResult = Assert.IsType<ActionResult<WeatherResult>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var weatherResult = Assert.IsAssignableFrom<WeatherResult>(okObjectResult.Value);

        // This section test indiviual properties and can be used to test mapping conditions
        Assert.Equal(weatherResult.City, _currentCity);
        Assert.True( !String.IsNullOrEmpty(weatherResult.ExecutionDateTime));
        Assert.NotNull(weatherResult.Weather);

    }

    [Fact]
    public async Task GetCityWeatherForecast_GetActionTarget_MustReturnOkObjectResult()
    {
        // Arrange

        var _weatherForecastServiceMock = new Mock<IWeatherForecaster>();
        _weatherForecastServiceMock
            .Setup(m => m.GetCurrentWeatherAsync(targetCity))
            .ReturnsAsync(new WeatherResult()
            {
                City = targetCity,
                ExecutionDateTime = DateTime.Now.ToString(),
                Weather = new WeatherCondition()
            });

        var _weatherForecastController = new WeatherForecastController(_weatherForecastServiceMock.Object,
                                                                   _loggerMock.Object);

        // Act
        var result = await _weatherForecastController.GetCityWeatherForecast(targetCity);

        // Assert

        // Get acton result
        var actionResult = Assert.IsType<ActionResult<WeatherResult>>(result);
        // Evaluate that teh type of respond is 'Ok'
        Assert.IsType<OkObjectResult>(actionResult.Result);

    }

    [Fact]
    public async Task GetCityWeatherForecast_GetAction_MustReturWeatherResult()
    {
        // Arrange

        var _weatherForecastServiceMock = new Mock<IWeatherForecaster>();
        _weatherForecastServiceMock
            .Setup(m => m.GetCurrentWeatherAsync(targetCity))
            .ReturnsAsync(new WeatherResult()
            {
                City = targetCity,
                ExecutionDateTime = DateTime.Now.ToString(),
                Weather = new WeatherCondition()
            });

        var _weatherForecastController = new WeatherForecastController(_weatherForecastServiceMock.Object,
                                                                   _loggerMock.Object);

        // Act
        var result = await _weatherForecastController.GetCityWeatherForecast(targetCity);

        // Assert

        // Get acton result
        var actionResult = Assert.IsType<ActionResult<WeatherResult>>(result);
        // This evaluate the response type WeatherResult
        Assert.IsAssignableFrom<WeatherResult>(
                ((OkObjectResult)actionResult.Result).Value);
    }

    [Fact]
    public async Task GetCityWeatherForecast_GetAction_MustReturWeatherResultWithValidProperties()
    {
        // Arrange

        var _weatherForecastServiceMock = new Mock<IWeatherForecaster>();
        _weatherForecastServiceMock
            .Setup(m => m.GetCurrentWeatherAsync(targetCity))
            .ReturnsAsync(new WeatherResult()
            {
                City = targetCity,
                ExecutionDateTime = DateTime.Now.ToString(),
                Weather = new WeatherCondition()
            });

        var _weatherForecastController = new WeatherForecastController(_weatherForecastServiceMock.Object,
                                                                   _loggerMock.Object);

        // Act
        var result = await _weatherForecastController.GetCityWeatherForecast(targetCity);

        // Assert

        // Get acton result
        var actionResult = Assert.IsType<ActionResult<WeatherResult>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var weatherResult = Assert.IsAssignableFrom<WeatherResult>(okObjectResult.Value);

        // This section test indiviual properties and can be used to test mapping conditions
        Assert.Equal(weatherResult.City, targetCity);
        Assert.True(!String.IsNullOrEmpty(weatherResult.ExecutionDateTime));
        Assert.NotNull(weatherResult.Weather);
    }

    [Fact]
    public async Task CreateWeatherSetting_IvalidInput_MustReturnOkObjectResult()
    {
        // Arrange

        var _weatherForecastServiceMock = new Mock<IWeatherForecaster>();
        _weatherForecastServiceMock
            .Setup(m => m.GetCurrentWeatherAsync(targetCity))
            .ReturnsAsync(new WeatherResult()
            {
                City = targetCity,
                ExecutionDateTime = DateTime.Now.ToString(),
                Weather = new WeatherCondition()
            });

        var _weatherForecastController = new WeatherForecastController(_weatherForecastServiceMock.Object,
                                                                   _loggerMock.Object);

        // Initialize DTO with default values, i.e. Empty values
        var weatherSetup = new WeatherSetupUpdate();

        // Add to the controller are ModelState in where the SettingName field is mandatory
        _weatherForecastController.ModelState
              .AddModelError("SettingName", "Required");


        // Act
        var result = await _weatherForecastController.CreateWeatherSetting(weatherSetup);

        // Assert

        // Get acton result
        var actionResult = Assert.IsType<ActionResult<WeatherSetupUpdate>>(result);

        BadRequestObjectResult? badREquestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.IsType<SerializableError>(badREquestResult.Value);
    }

}
