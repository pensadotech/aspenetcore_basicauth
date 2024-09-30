using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApiBasicAuth.Filters;

// Filter to remove from documentation tex/plain opetions
public class RemovePlainTextMediaTypeFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Responses == null) return;

        foreach (var response in operation.Responses)
        {
            var content = response.Value.Content;

            if (content.ContainsKey("text/plain"))
            {
                content.Remove("text/plain");
            }
        }
    }
}