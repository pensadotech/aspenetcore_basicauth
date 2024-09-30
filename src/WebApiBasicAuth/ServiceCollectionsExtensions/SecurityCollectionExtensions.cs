using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApiBasicAuth.Security;

namespace WebApiBasicAuth.ServiceCollectionsExtensions;

// Services extension to add security to teh API, in this case Basic Auth

public static class SecurityCollectionExtensions
{
    public static IServiceCollection AddBasicSecurity(this IServiceCollection services, IConfiguration config)
{
    // Set basic authentication handler to confront credentials
    services.AddAuthentication("Basic")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

    // TODO: Add and configure authorization services / is this needed?
    //  services.AddAuthorization();

    // TODO: Authorization Policies?


    return services;
}
}
