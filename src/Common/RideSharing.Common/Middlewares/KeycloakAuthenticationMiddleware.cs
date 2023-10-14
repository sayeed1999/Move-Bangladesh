using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using RideSharing.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace RideSharing.Common.Middlewares
{
    /// <summary>
    /// This middleware validates a jwt token and authorizes a user from 'keycloak' identity provider.
    /// </summary>
    public class KeycloakAuthenticationMiddleware : IMiddleware
    {
        private readonly ILogger<KeycloakAuthenticationMiddleware> logger;
        private readonly KeycloakConfig _config;
        private TokenValidationParameters _validationParameters;

        public KeycloakAuthenticationMiddleware(ILogger<KeycloakAuthenticationMiddleware> logger, IOptions<KeycloakConfig> config)
        {
            this.logger = logger;
            _config = config.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                var headers = context.Request.Headers;
                if (headers.ContainsKey("Authorization"))
                {
                    var authorization = headers["Authorization"];
                    var bearerHeader = AuthenticationHeaderValue.Parse(authorization);
                    await Authenticate(context, bearerHeader, logger).ConfigureAwait(false);
                }
                await next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                await context.Response.WriteAsync(ex.Message);
            }
        }

        public async Task<ClaimsPrincipal> Authenticate(HttpContext context, AuthenticationHeaderValue authenticationHeader, ILogger logger)
        {
            var token = authenticationHeader.Parameter;
            try
            {
                var (_, principal) = await Validate(token).ConfigureAwait(false);
                context.Items.Add("roles", principal.FindAll(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList());
                context.Items.Add("name", principal.Claims.First(c => c.Type == "name").Value);
                context.Items.Add("email", principal.Claims.First(c => c.Type == ClaimTypes.Email).Value);
                context.Items.Add("isAuthenticated", principal.Identity.IsAuthenticated);
                return principal;
            }
            catch (SecurityTokenExpiredException ex)
            {
                logger.LogError(ex, "Token has expired");
                throw new UnauthorizedAccessException("Token has expired.");
            }
            catch (SecurityTokenInvalidAudienceException ex)
            {
                logger.LogError(ex, "Invalid audience");
                throw new UnauthorizedAccessException("User is not authorized.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to validate token");
                throw new UnauthorizedAccessException("User is not authorized.");
            }
        }

        public async Task<(JwtSecurityToken, ClaimsPrincipal)> Validate(string token)
        {
            await ConfigureValidation().ConfigureAwait(false);

            var tokenHandler = new JwtSecurityTokenHandler();
            var result = tokenHandler.ValidateToken(token, _validationParameters, out var jwt);

            return (jwt as JwtSecurityToken, result);
        }

        private async Task ConfigureValidation()
        {
            var serviceDiscoveryEndpoint = $"{_config.AuthUrl}/realms/{_config.Tenant}/.well-known/openid-configuration";
            var configManager = new ConfigurationManager<OpenIdConnectConfiguration>(serviceDiscoveryEndpoint, new OpenIdConnectConfigurationRetriever());
            var oidcConfig = await configManager.GetConfigurationAsync().ConfigureAwait(false);
            var validAudiences = new string[] { "realm-management", "account" };

            _validationParameters = new TokenValidationParameters
            {
                ValidAudiences = validAudiences,
                ValidateAudience = true,
                ValidateIssuer = true,
                IssuerSigningKeys = oidcConfig.SigningKeys,
                ValidIssuer = oidcConfig.Issuer,
                ValidateLifetime = true
            };
        }
    }
}
