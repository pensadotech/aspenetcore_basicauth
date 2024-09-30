# Programming Notes
**Web API with Xunit Test using a Basic Authentication example** 

_By Armando A. Pensado_

### Characteristics and NuGet dependencies
APS.NET CORE Application using .NET 8 and C#

Basic level


**NuGet dependencies**
* Asp.Versioning.Mvc
* Asp.Versioning.Mvc.ApiExplorer
* Swashbuckle.AspNetCore
* Swashbuckle.AspNetCore.Annotations
* AutoMapper


## Description 

This is a simple Web API project that provides Xunit examples using a basic authentication application. It also includes OpenAPI (Swagger) documentation and  including API versioning.

It is important to mention that basic authentication is the most simple authentication method, and must be used under HTTPS protocol, to gain some benefits from the certificates encryptions. 


## How does it works

Under src\WebApiBasicAuth the Web API, the Program.cs, add a services to enable basic authentication, enforcing validation thorugh a handler. The handler is a program that obtain credentials from the header message and appsettings.Development.json, comparing them both before allowing the request to continue. 

However the focus of attention in here is the use of Xunit, and under src\WebApiBasicAuth.Tests a series of unit test are shared with the reader.. 

Under src\postman a Postman colection is included to exercise the Web API. The reader must ensure that teh proepr port is aligned between teh applicaiton and the Postman colelction. The prot definitions are inside the src\WebApiBasicAuth\propeties\launchSettings.json


The configuration points in the Program.cs to enfoce Basic Auth are as follows:

```c#
// SERVICES
// Set basic authentication handler to confront credentials
builder.Services.AddAuthentication("Basic")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

// MIDDELWARE
app.UseAuthentication();
app.UseAuthorization();
```

At the level of the controllers, it is required to mark properly what requires authorization. It can also be set at the level of the methods inside the controllers. 

To block the full controller, the 'Authorize' attribute is set as follows

```c#
using Microsoft.AspNetCore.Authorization;

namespace WebApiBasicAuth.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/WeatherForecast")]
[ApiVersion("1.0")]
[Authorize]  // Can only be accesed with proper authnetication
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecaster _weatherForecaster;
    private readonly ILogger<WeatherForecastController> _logger;

  ....
}
``

Then, the caller needs to send, under basic authentication model, the id and password in the message hdeader. For this example, and usong Postman, it will be: 

```C#
Auth Type : Basict Auth
Username  : testuser01
Password  : abc123
```

As the request message comes in, and the Authentication is enforced, the middleware will redirect the message to the handler program. In this case 'BasicAuthenticationHandler'.

In the hamdler, credentials in the setting files are extracted

```c#
// BasicAuthenticationHandler.cs

_userId = _config.GetValue<string>("Security:userIdentification");
_userSecret = _config.GetValue<string>("Security:userPassword");
```

also, the request id and password is extracted

```c#
// BasicAuthenticationHandler.cs

// Get Authrization value from hreade, it comes as userid:password 64 bit encoded
var authenticationHeader = AuthenticationHeaderValue
                                .Parse(Request.Headers["Authorization"]);

// Decode the basic auth credentials
byte[]? credentialBytes = Convert.FromBase64String(authenticationHeader.Parameter);
string[]? credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

// Retreive user id and passoword
var username = credentials[0];
var password = credentials[1];
```

Then the handler proceed to compare them and if these matches, then it returns a succesful signal. Otherwise, the middleware flow will send back a fail conditions, and if the controllers are properly configured, the applicaiton will send back a 401 Unauthorized.


## How developers can get started

The developer can dowloand the source code and it si recommedned to download the applicaiton Postman for testing.


Posman collection
Weather Forecaster Basic Auth.postman_collection.json


## References

[Postman](https://www.postman.com/)

[Basic Authentication in ASP.NET Web API](https://learn.microsoft.com/en-us/aspnet/web-api/overview/security/basic-authentication)

[BASIC AUTHENTICATION IN ASP.NET CORE](https://damienbod.com/2023/01/23/basic-authentication-in-asp-net-core/)

[Simple authorization in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/simple?view=aspnetcore-8.0)