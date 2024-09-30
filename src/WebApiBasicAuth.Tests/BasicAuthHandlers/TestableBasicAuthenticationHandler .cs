using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using WebApiBasicAuth.Security;

namespace WebApiBasicAuth.Tests.BasicAuthHandlers;

// For unit test, the BasicAuthenticationHandler has a protected mthod
// that is not reachable, teh example here is a wrapper that inherits from 
// from teh class, and overrides the method, making it accessible 
// for the unit tets logic.
//
// This is one approach, but an alterative way in the unit test, this could
// moq using the "Prottected" attrribute.

public class TestableBasicAuthenticationHandler : BasicAuthenticationHandler
{
    public TestableBasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IConfiguration config)
        : base(options, logger, encoder, clock, config)
    {

    }

    public new Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        return base.HandleAuthenticateAsync();
    }

}
