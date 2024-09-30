using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;

namespace WebApiBasicAuth.ServiceCollectionsExtensions;

public static class ControllersOptionsCollectionExtension
{

    public static IServiceCollection AddControllerOptionFilters(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers(options =>
        {
            // General errors across all corollers: 400, 406, 500
            // This errors are wil apply possible bad request errors accross all controllers
            // This behavior will overwrite API conventions 
            options.Filters.Add(
                new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
            options.Filters.Add(
                new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
            options.Filters.Add(
                new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
            options.Filters.Add(
                new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
            options.Filters.Add(
                new ProducesDefaultResponseTypeAttribute());

            // Returns a 406 not acceptable, if requested media type is not supported
            options.ReturnHttpNotAcceptable = true;

            // Remove "text/json" media type
            var jsonOutputFormatter = options.OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()
                .FirstOrDefault();

            if (jsonOutputFormatter != null)
            {
                jsonOutputFormatter.SupportedMediaTypes.Remove("text/json");
            }

            var jsonInputFormatter = options.InputFormatters
                .OfType<SystemTextJsonInputFormatter>()
                .FirstOrDefault();

            if (jsonInputFormatter != null)
            {
                jsonInputFormatter.SupportedMediaTypes.Remove("text/json");
            }

        });

        return services;

    }
}
