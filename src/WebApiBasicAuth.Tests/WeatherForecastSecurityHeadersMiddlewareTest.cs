using Microsoft.AspNetCore.Http;
using WebApiBasicAuth.Middleware;


namespace WebApiBasicAuth.Tests;

// This unit test exercises a custom middleware. This middleware is 
// not at play inside the Program.cs, but is added here as an example
// for creating and testing custmoe middleware

public class WeatherForecastSecurityHeadersMiddlewareTest
{
    [Fact]
    public async Task InvokeAsync_Invoke_SetsExpectedResponseHeaders()
    {
        // Arrange

        var httpContext = new DefaultHttpContext();
        // Create a Next delegae that returns a Task Completed,
        // to avoid calling anything else.
        RequestDelegate next = (HttpContext httpContext) => Task.CompletedTask;

        var middleware = new WeatherForecastSecurityHeadersMiddleware(next);

        // Act
        await middleware.InvokeAsync(httpContext);

        // Assert
        var cspHeader = httpContext.Response.Headers["Content-Security-Policy"].ToString();
        var xContentTypeOptionsHeader = httpContext.Response.Headers["X-Content-Type-Options"].ToString();

        Assert.Equal("default-src 'self';frame-ancestors 'none';", cspHeader);
        Assert.Equal("nosniff", xContentTypeOptionsHeader);
    }
}
