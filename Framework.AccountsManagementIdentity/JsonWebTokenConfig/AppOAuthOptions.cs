using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.AccountsManagementIdentity.JsonWebTokenConfig
{
    public class AppOAuthOptions : OAuthAuthorizationServerOptions
    {
        public AppOAuthOptions(IAppJwtConfiguration configuration)
        {
            this.AllowInsecureHttp = true; // TODO: Buy an SSL certificate!
            this.TokenEndpointPath = new PathString(configuration.TokenPath);
            this.AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(configuration.ExpirationMinutes);
            this.AccessTokenFormat = new AppJwtWriterFormat(this, configuration);
            this.Provider = SmObjectFactory.Container.GetInstance<IOAuthAuthorizationServerProvider>();
            this.RefreshTokenProvider = SmObjectFactory.Container.GetInstance<IAuthenticationTokenProvider>();
        }
    }
}
