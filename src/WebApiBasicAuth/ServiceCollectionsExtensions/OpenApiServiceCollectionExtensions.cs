using Asp.Versioning.ApiExplorer;
using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace WebApiBasicAuth.ServiceCollectionsExtensions;

public static class OpenApiServiceCollectionExtensions
{
    public static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services)
    {

        // Configure API versioning (using Asp.Versioning.Mvc and Asp.Versioning.Mvc.ApiExplorer).
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddSwaggerGen(c =>
        {
            // Discover versions within the application
            var provider = services.BuildServiceProvider()
                                           .GetRequiredService<IApiVersionDescriptionProvider>();

            if (provider != null)
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Title = $"Auth Web Server API {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = description.IsDeprecated ? "This API version has been deprecated." : null,
                        Contact = new OpenApiContact()
                        {
                            Email = "Pensadotech@gmail.com",
                            Name = "Pensadotech",
                            Url = new Uri("https://www.twitter.com/Pensadotech")
                        },
                        License = new OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });
                }
            }

            // Using Swashbuckle.AspNetCore.Annotations and adding to 
            // each method, [SwaggerOperation(Summary = "myDescription, OperationId = "Post_Person")] 
            // REf: https://medium.com/c-sharp-progarmming/configure-annotations-in-swagger-documentation-for-asp-net-core-api-8215596907c7
            c.EnableAnnotations();

            // For swagger to consider basic auth
            c.AddSecurityDefinition("basicAuth", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "basic",
                Description = "Input your username and password to access this API"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basicAuth" }
                        }, new List<string>() }
                });
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();


        return services;
    }

}
