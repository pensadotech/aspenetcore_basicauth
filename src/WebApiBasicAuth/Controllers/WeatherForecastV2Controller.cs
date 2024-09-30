using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApiBasicAuth.Domain.Entities;
using WebApiBasicAuth.Domain.Models;
using WebApiBasicAuth.Services;

namespace WebApiBasicAuth.Controllers;

// Examaple controler to ilustrate versioning and documentation.
// It drives a seimple service for getting a weather information for a city.

[ApiController]
[Route("api/v{version:apiVersion}/WeatherForecast")]
[ApiVersion("2.0")]
[Authorize]
public class WeatherForecastV2Controller : ControllerBase
{
    private readonly IWeatherForecaster _weatherForecaster;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastV2Controller(IWeatherForecaster weatherForecaster,
        ILogger<WeatherForecastController> logger)
    {
        _weatherForecaster = weatherForecaster;
        _logger = logger;
    }

    [HttpGet()]
    [SwaggerOperation(OperationId = "GetCurrentWeatherForecast", Summary = "Get weather condition for current city")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WeatherResult>> Get()
    {
        // Pretend a larger process
        Task.Delay(TimeSpan.FromSeconds(10));

        // Get default weather conditions
        WeatherResult? currentWeather = await _weatherForecaster.GetCurrentWeatherAsync("Current City");

        return Ok(currentWeather);
    }

    [HttpGet("{city}")]
    [SwaggerOperation(OperationId = "GetCityWeatherForecast", Summary = "Get weather condition for selected city")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WeatherResult>> Get(string city)
    {
        // Pretend a larger process
        Task.Delay(TimeSpan.FromSeconds(10));

        // Get specifict city weather conditions
        WeatherResult? currentWeather = await _weatherForecaster.GetCurrentWeatherAsync(city);

        return Ok(currentWeather);
    }

    [HttpPost()]
    [SwaggerOperation(OperationId = "CreateWeatherSetting", Summary = "Add a new setting for the weather station")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WeatherSetupUpdate>> CreateWeatherSetting(
        WeatherSetupUpdate weatherSetting)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Pretend a larger process
        Task.Delay(TimeSpan.FromSeconds(10));

        return Ok(weatherSetting);
    }

    [HttpPut()]
    [SwaggerOperation(OperationId = "UpdateWeatherSetting", Summary = "update a setting for the weather station")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WeatherSetupUpdate>> UpdateWeatherSetting(
        WeatherSetupUpdate weatherSetting)
    {
        // Pretend a larger process
        Task.Delay(TimeSpan.FromSeconds(10));

        return Ok(weatherSetting);
    }

    [HttpDelete("{settingId}")]
    [SwaggerOperation(OperationId = "DeleteWeatherSetting", Summary = "Delete a setting fro teh weather station")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WeatherSetupUpdate>> DeleteWeatherSetting(string settingId)
    {
        // Pretend a larger process
        Task.Delay(TimeSpan.FromSeconds(10));

        return Ok();
    }


}
