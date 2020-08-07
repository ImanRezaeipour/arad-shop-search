using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.IdentityModel.Tokens;
using System.Text;

namespace Framework.AccountsManagementIdentity.JsonWebTokenConfig
{
    internal class AppJwtWriterFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly OAuthAuthorizationServerOptions _options;
        private readonly IAppJwtConfiguration _configuration;

        public AppJwtWriterFormat(OAuthAuthorizationServerOptions options, IAppJwtConfiguration configuration)
        {
            _options = options;
            _options.CheckArgumentNull(nameof(_options));

            _configuration = configuration;
            _configuration.CheckArgumentNull(nameof(_configuration));
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_options.AccessTokenExpireTimeSpan.TotalMinutes);
            var symmetricKey = Encoding.UTF8.GetBytes(_configuration.JwtKey);
            var signingCredentials = new SigningCredentials(new InMemorySymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);
            var token = new JwtSecurityToken(_configuration.JwtIssuer, _configuration.JwtAudience, data.Identity.Claims,
                                             now, expires, signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}