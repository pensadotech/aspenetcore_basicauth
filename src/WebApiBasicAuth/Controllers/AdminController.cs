using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiBasicAuth.Controllers;

// This controller was created to ilustrate unit testing using a
// http context with a defined Pricnipal. 

[Route("api/v{version:apiVersion}/admin")]
[ApiVersion("1.0")]
[ApiController]
public class AdminController : ControllerBase
{
    [HttpGet]
    [Authorize]
    // [MyCustomAuthorizeAttribute]
    // [Authorize(Policy = "MyPolicy"]
    public IActionResult GetProtectedWeatherInfo()
    {
        // depending on the role, redirect to another action
        if (User.IsInRole("Admin"))
        {
            // Redirect(<Method-name>,<Controller-name>)

            return RedirectToAction(
                "GetCurrentWeatherForecast", "WeatherForecast");
        }

        return RedirectToAction("GetStatistics", "Statistics");
    }
}
