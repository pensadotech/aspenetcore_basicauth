using Asp.Versioning.ApiExplorer;
using WebApiBasicAuth.ServiceCollectionsExtensions;

// ref: https://medium.com/@softsusanta/net-core-api-versioning-implementation-step-by-step-92107e447798


var builder = WebApplication.CreateBuilder(args);

// SERVICES (add to DI container)

// Use service collection extensions
builder.Services
         .AddControllerOptionFilters(builder.Configuration)         // Add web interface controller filter options
         .AddWeatherServices(builder.Configuration)                 // Add weather services
         .AddBasicSecurity(builder.Configuration)                   // Set basic authentication handler to confront credentials
         .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())    // Add auto mapper
         .AddOpenApiDocumentation()                                 // add AutoMapper for mapping between entities and DTOs
         .AddProblemDetails();                                      // Enable exception details


// MIDDLEWARE

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Execute develoeprs error page
    //app.UseDeveloperExceptionPage();

    // Swager activation can be added here to be enabled 
    // under development settings. For overal use, set outside

}

// Configure the HTTP request pipeline.

// Swagger activation
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // Disvover versionin within the application
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    // Swagger json file location per version
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                                      description.GroupName.ToUpperInvariant());
    }

    // Set Swagger UI at app's root
    // Inside the launchSettings.json, set  "launchUrl": ""
    options.RoutePrefix = string.Empty; 
});


app.UseHttpsRedirection();

// Security
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
