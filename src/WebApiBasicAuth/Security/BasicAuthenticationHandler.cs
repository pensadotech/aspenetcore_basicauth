using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace WebApiBasicAuth.Security;

// Handler to evaluate Basic auth credentials

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private string _userId;   
    private string _userSecret;

    private IConfiguration _config;

    public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration config)
            : base(options, logger, encoder, clock)
    {
        _config = config;
        _userId = _config.GetValue<string>("Security:userIdentification");
        _userSecret = _config.GetValue<string>("Security:userPassword");
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
        }

        try
        {
            // Get Authrization value from hreade, it comes as userid:password 64 bit encoded
            var authenticationHeader = AuthenticationHeaderValue
                                            .Parse(Request.Headers["Authorization"]);

            // Decode the basic auth credentials
            byte[]? credentialBytes = Convert.FromBase64String(authenticationHeader.Parameter);
            string[]? credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

            // Retreive user id and passoword
            var username = credentials[0];
            var password = credentials[1];


            // Evaluate authentication based internal userid and pwd
            if (username == _userId && password == _userSecret)
            {
                var claims = new[] { new Claim(ClaimTypes.NameIdentifier, username) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return Task.FromResult(AuthenticateResult.Success(ticket));
            }

            return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
        }
        catch
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization header"));
        }
    }


}
