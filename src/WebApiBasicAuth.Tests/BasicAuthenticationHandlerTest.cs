using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using WebApiBasicAuth.Tests.BasicAuthHandlers;
using WebApiBasicAuth.Tests.Fixtures;
using Xunit.Abstractions;

namespace WebApiBasicAuth.Tests;

// This unit test exercises the funciotnality that enforces basic authentication
// A BasicAuthenticationHandler is created for the the web api.

// However, it contains a protected method that cannot be reached using regular
// test logic. It can be accesses using Moq functionality.
// Nevertheless, for ilustration purposes, a TestableBasicAuthenticationHandler was created
// to work around the limitation. 

// Because tehre are commone elements for teh test contained in here, a Fixture was created
// to make teh test more efficient. If the same common elements are needed across multiple
// test classes, a Fixture collection could be used.


// NOTE: For simple test fixture implement IClassFixture<T>
//       for collection fixture, use attribute [Collection(<fixture-name")] and
//       comment the use of IClassFixture

// [Collection("BasicAuthenticationHandlerCollection")]
public class BasicAuthenticationHandlerTest  : IClassFixture<BasicAuthenticationHandlerFixture>
{
    private readonly BasicAuthenticationHandlerFixture _BasicAuthenticationHandlerFixture;
    private readonly ITestOutputHelper _testOutputHelper;

    public BasicAuthenticationHandlerTest(BasicAuthenticationHandlerFixture basicAuthenticationHandlerFixture,
        ITestOutputHelper testOutputHelper)
    {
        // Set fixture access
        _BasicAuthenticationHandlerFixture = basicAuthenticationHandlerFixture;

        // To provide execution logs in the test pop-up 
        // not the test explorer, but teh pop-up on top of eh test itself.
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task HandleAuthenticateAsync_ValidCredentials_ReturnsSuccess()
    {
        // ARRANGE

        _testOutputHelper.WriteLine($"Set authenticator handler elements");

        // Set mock options for authetication handler
        var options = _BasicAuthenticationHandlerFixture.Handler_Options;

        // Set mock logger for authetication handler
        var logger = _BasicAuthenticationHandlerFixture.Handler_Logger;

        // Set mock encoder ad clock for authetication handler
        var encoder = _BasicAuthenticationHandlerFixture.Handler_Encoder;
        var clock = _BasicAuthenticationHandlerFixture.Handler_Clock;

        // Set memoy configuration with pretend values coming from settings
        IConfiguration config = _BasicAuthenticationHandlerFixture.Handler_Config;


        // Prented this are the provider credentials for the API
        string _userId = "testuser01";
        string _userSecret = "abc123";

       _testOutputHelper.WriteLine($"Set pretned provided credentials for : " + $"{_userId}");


        // Initialize basic auth handler and context
        var handler = new TestableBasicAuthenticationHandler(options.Object, logger.Object, encoder.Object, clock.Object, config);
        var scheme = new AuthenticationScheme("Basic", "Basic", typeof(TestableBasicAuthenticationHandler));
        var context = new DefaultHttpContext();

        // add pretend provider credentials to message header and create request messahe
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(_userId + ":" + _userSecret));
        context.Request.Headers["Authorization"] = $"Basic {credentials}";
        context.RequestServices = new ServiceCollection().BuildServiceProvider();

        // initialize handler
        await handler.InitializeAsync(scheme, context);

        _testOutputHelper.WriteLine($"Handler is initialized");

        // ACT
        var result = await handler.AuthenticateAsync();

        // ASSERT
        Assert.True(result.Succeeded);
        Assert.Equal(_userId, result.Principal.Claims.First().Value);

        // Assert.Equal("testuser01", result.Principal.Identity.Name); - THIS DO NOT WORK, result.Principal.Identity.Name coens null
    }

    [Fact]
    public async Task HandleAuthenticateAsync_InvalidCredentials_ReturnsFail()
    {
        // ARRANGE

        _testOutputHelper.WriteLine($"Set authenticator handler elements");

        // Set mock options for authetication handler
        var options = _BasicAuthenticationHandlerFixture.Handler_Options;

        // Set mock logger for authetication handler
        var logger = _BasicAuthenticationHandlerFixture.Handler_Logger;

        // Set mock encoder ad clock for authetication handler
        var encoder = _BasicAuthenticationHandlerFixture.Handler_Encoder;
        var clock = _BasicAuthenticationHandlerFixture.Handler_Clock;

        // Set memoy configuration with pretend values coming from settings
        IConfiguration config = _BasicAuthenticationHandlerFixture.Handler_Config;


        // Prented this are the provider credentials for the API
        string _userId = "testuser01";
        string _userSecret = "abc12345";

        _testOutputHelper.WriteLine($"Set pretned provided credentials for : " + $"{_userId}");

        // Initialize basic auth handler and context
        var handler = new TestableBasicAuthenticationHandler(options.Object, logger.Object, encoder.Object, clock.Object, config);
        var scheme = new AuthenticationScheme("Basic", "Basic", typeof(TestableBasicAuthenticationHandler));
        var context = new DefaultHttpContext();

        // add pretend provider credentials to message header and create request messahe
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(_userId + ":" + _userSecret));
        context.Request.Headers["Authorization"] = $"Basic {credentials}";
        context.RequestServices = new ServiceCollection().BuildServiceProvider();

        // initialize handler
        await handler.InitializeAsync(scheme, context);

        _testOutputHelper.WriteLine($"Handler is initialized");

        // ACT
        var result = await handler.AuthenticateAsync();


        // Assert
        Assert.False(result.Succeeded);
    }

    [Fact]
    public async Task HandleAuthenticateAsync_MissingAuthorizationHeader_ReturnsFail()
    {
        // ARRANGE

        _testOutputHelper.WriteLine($"Set authenticator handler elements");

        // Set mock options for authetication handler
        var options = _BasicAuthenticationHandlerFixture.Handler_Options;

        // Set mock logger for authetication handler
        var logger = _BasicAuthenticationHandlerFixture.Handler_Logger;

        // Set mock encoder ad clock for authetication handler
        var encoder = _BasicAuthenticationHandlerFixture.Handler_Encoder;
        var clock = _BasicAuthenticationHandlerFixture.Handler_Clock;

        // Set memoy configuration with pretend values coming from settings
        IConfiguration config = _BasicAuthenticationHandlerFixture.Handler_Config;

        // Prented that no credentials are provided
        _testOutputHelper.WriteLine($"Pretend tehr eare no credentials ");


        // Initialize basic auth handler and context
        var handler = new TestableBasicAuthenticationHandler(options.Object, logger.Object, encoder.Object, clock.Object, config);
        var scheme = new AuthenticationScheme("Basic", "Basic", typeof(TestableBasicAuthenticationHandler));
        var context = new DefaultHttpContext();

        context.RequestServices = new ServiceCollection().BuildServiceProvider();

        // initialize handler
        await handler.InitializeAsync(scheme, context);

        _testOutputHelper.WriteLine($"Handler is initialized");

        // Act
        var result = await handler.HandleAuthenticateAsync();

        // Assert
        Assert.False(result.Succeeded);

        Assert.Equal("Missing Authorization Header", result.Failure.Message);
    }

}
