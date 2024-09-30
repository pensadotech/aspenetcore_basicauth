using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebApiBasicAuth.Tests.BasicAuthHandlers;

namespace WebApiBasicAuth.Tests.Fixtures;

// This fixture defined common elements for the multile test 
// inside BasicAuthenticationHandlerTest.
//
// The element defined here are used to initialize a 
// basic auth handler

public class BasicAuthenticationHandlerFixture : IDisposable
{
    public Mock<IOptionsMonitor<AuthenticationSchemeOptions>> Handler_Options 
        { get; }

    public Mock<ILoggerFactory> Handler_Logger 
       { get; }

    public Mock<UrlEncoder> Handler_Encoder 
       { get; }

    public Mock<ISystemClock> Handler_Clock 
       { get; }

    public IConfiguration Handler_Config 
       { get; }


    public BasicAuthenticationHandlerFixture()
    {

        // Set mock options for authetication handler
        Handler_Options = new Mock<IOptionsMonitor<AuthenticationSchemeOptions>>();
        Handler_Options.Setup(o => o.Get(It.IsAny<string>())).Returns(new AuthenticationSchemeOptions());

        // Set mock logger for authetication handler
        var logerHdlr = new Mock<ILogger<TestableBasicAuthenticationHandler>>();
        Handler_Logger = new Mock<ILoggerFactory>();
        Handler_Logger.Setup(x => x.CreateLogger(It.IsAny<string>()))
                      .Returns(logerHdlr.Object);

        // Set mock encoder ad clock for authetication handler
        Handler_Encoder = new Mock<UrlEncoder>();
        Handler_Clock = new Mock<ISystemClock>();

        // In-memory Config (target credentials)
        // Set pretned reference values coming from configuraiton file
        string _refUserId = "testuser01";
        string _refUserSecret = "abc123";

        var inMemorySettings = new Dictionary<string, string> {
            {"Security:userIdentification", _refUserId},
            {"Security:userPassword", _refUserSecret},
        };

        Handler_Config = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings) // Add in-memory settings
            .Build();

        // Alternative option for adding in memoty configuration
        //var Handler_Config = new ConfigurationBuilder()
        //        .AddInMemoryCollection(
        //            new Dictionary<string, string> {
        //               {"Security:userIdentification", _refUserId},
        //               {"Security:userPassword", _refUserSecret},
        //            })
        //        .Build();

    }

    public void Dispose()
    {
        // clean up the setup code, for unmanaged resources, if required
    }

}
