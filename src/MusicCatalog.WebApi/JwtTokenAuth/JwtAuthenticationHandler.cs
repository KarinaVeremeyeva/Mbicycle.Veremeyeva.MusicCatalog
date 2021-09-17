using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MusicCatalog.WebApi.JwtTokenAuth
{
    /// <summary>
    /// Jwt authentication handler
    /// </summary>
    public class JwtAuthenticationHandler : AuthenticationHandler<JwtAuthenticationOptions>
    {
        /// <summary>
        /// Jwt authentication handler constructor
        /// </summary>
        /// <param name="options">Options</param>
        /// <param name="logger">Logger</param>
        /// <param name="encoder">Encoder</param>
        /// <param name="clock">Clock</param>
        public JwtAuthenticationHandler(
            IOptionsMonitor<JwtAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        /// <summary>
        /// Allows derived types to handle authentication
        /// </summary>
        /// <returns>AuthenticateResult</returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:2563");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var response = await httpClient.GetAsync($"api/Users/validate?token={token}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var identity = new ClaimsIdentity(jwtToken.Claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Unauthorized");
        }
    }
}

