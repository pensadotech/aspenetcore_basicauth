namespace WebApiBasicAuth.Middleware;

// THis is an example of a milldeware, which is notimplemented in teh Program.cs
// This is used as an example to uni test custome middleware.

public class WeatherForecastSecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public WeatherForecastSecurityHeadersMiddleware(RequestDelegate nextRequestDelegate)
    {
        _next = nextRequestDelegate;   
    }

    public async Task InvokeAsync(HttpContext context)
    {
        IHeaderDictionary headers = context.Response.Headers;

        // Add CSP + X-Content-Type
        headers["Content-Security-Policy"] = "default-src 'self';frame-ancestors 'none';";
        headers["X-Content-Type-Options"] = "nosniff";

        await _next(context);
    }

}
