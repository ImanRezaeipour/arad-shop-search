using Common.Infrastructure.Persistance.EF;
using Framework.AccountsManagementIdentity.UserHelper;
using Framework.Core.Helper;
using Framework.Persistance.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Framework.AccountsManagementIdentity.Providers
{
    internal class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        //public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //    #region Set Allow-Origin in Header

        //    //////var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
        //    //////if (allowedOrigin == null) allowedOrigin = "*";
        //    //////context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

        //    #endregion Set Allow-Origin in Header

        //    #region Check User

        //    var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
        //    User user = await userManager.FindAsync(context.UserName, context.Password);

        //    if (user == null)
        //    {
        //        context.SetError("invalid_grant", "The user name or password is incorrect.");
        //        context.Response.Headers.Add(ServerGlobalVariables.OwinChallengeFlag,
        // new[] { ((int)HttpStatusCode.Unauthorized).ToString() });
        //        context.Rejected();
        //        return;
        //    }

        //    #endregion Check User

        //    #region Test

        //    //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        //    //identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
        //    //identity.AddClaim(new Claim("sub", context.UserName));
        //    //identity.AddClaim(new Claim("role", "user"));

        //    #endregion Test

        //    HttpContext.Current.GetOwinContext().Get<IUnitOfWork>();

        //    ClaimsIdentity oAuthIdentity = await GenerateUserIdentityAsync(user, userManager, OAuthDefaults.AuthenticationType);
        //    //ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager, CookieAuthenticationDefaults.AuthenticationType);
        //    AuthenticationProperties properties = CreateProperties(user.UserName, (context.ClientId == null) ? string.Empty : context.ClientId);
        //    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
        //    context.Validated(ticket);
        //    //context.Request.Context.Authentication.SignIn(cookiesIdentity);
        //    //context.Request.Context.Authentication.SignIn(oAuthIdentity);   //TODO: Bayad Bedeghat Baresi shavad Cookies Comment Shod kharab kari nashe
        //}

        //public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        //{
        //    #region Validate client_id

        //    var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
        //    var currentClient = context.ClientId;

        //    if (originalClient != currentClient)
        //    {
        //        context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
        //        return;
        //    }

        //    #endregion Validate client_id

        //    #region Check User

        //    var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
        //    User user = await userManager.FindByNameAsync(context.Ticket.Identity.Name);

        //    if (user == null)
        //    {
        //        context.SetError("invalid_grant", "The user name or password is incorrect.");
        //        return;
        //    }

        //    #endregion Check User

        //    ClaimsIdentity oAuthIdentity = await GenerateUserIdentityAsync(user, userManager, OAuthDefaults.AuthenticationType);
        //    AuthenticationProperties properties = CreateProperties(user.UserName, (context.ClientId == null) ? string.Empty : context.ClientId);
        //    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
        //    context.Validated(ticket);

        //    #region agar Bekhahim faghat be haman Tikent ghabli ye chizi ezafe konim az in ravesh miravim

        //    // Change auth ticket for refresh token requests
        //    //var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
        //    //newIdentity.AddClaim(new Claim("newClaim", "newValue"));

        //    //var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
        //    //context.Validated(newTicket);

        //    //return Task.FromResult<object>(null);

        //    #endregion agar Bekhahim faghat be haman Tikent ghabli ye chizi ezafe konim az in ravesh miravim
        //}

        //public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        //{
        //    foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
        //    {
        //        context.AdditionalResponseParameters.Add(property.Key, property.Value);
        //    }

        //    return Task.FromResult<object>(null);
        //}

        //public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        //{
        //    string clientId = string.Empty;
        //    string clientSecret = string.Empty;
        //    Client client = null;

        //    //if (context.Request.Method == "OPTIONS")
        //    //{
        //    //    //In case of an OPTIONS, we allow the access to the origin of the petition

        //    //    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
        //    //    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "POST" });
        //    //    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "accept, content-type" });
        //    //    context.OwinContext.Response.Headers.Add("Access-Control-Max-Age", new[] { "1728000" });

        //    //}

        //    if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
        //    {
        //        context.TryGetFormCredentials(out clientId, out clientSecret);
        //    }

        //    if (context.ClientId == null)
        //    {
        //        //Remove the comments from the below line context.SetError, and invalidate context
        //        //if you want to force sending clientId/secrects once obtain access tokens.
        //        context.Validated();
        //        //context.SetError("invalid_clientId", "ClientId should be sent.");
        //        return Task.FromResult<object>(null);
        //    }

        //    using (AuthRepository _repo = new AuthRepository())
        //    {
        //        client = _repo.FindClient(context.ClientId);
        //    }

        //    if (client == null)
        //    {
        //        context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
        //        return Task.FromResult<object>(null);
        //    }

        //    if (client.ApplicationType == ApplicationTypes.NativeConfidential)
        //    {
        //        if (string.IsNullOrWhiteSpace(clientSecret))
        //        {
        //            context.SetError("invalid_clientId", "Client secret should be sent.");
        //            return Task.FromResult<object>(null);
        //        }
        //        else
        //        {
        //            if (client.Secret != CustomHash.GetHash(clientSecret))
        //            {
        //                context.SetError("invalid_clientId", "Client secret is invalid.");
        //                return Task.FromResult<object>(null);
        //            }
        //        }
        //    }

        //    if (!client.Active)
        //    {
        //        context.SetError("invalid_clientId", "Client is inactive.");
        //        return Task.FromResult<object>(null);
        //    }

        //    context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
        //    context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

        //    context.Validated();
        //    return Task.FromResult<object>(null);
        //}

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName, string clientId)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "as:client_id", clientId },
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync( User _user , UserManager<User, Int64> manager, string authenticationType)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(_user, authenticationType);

        //    userIdentity.AddClaim(new Claim("SessionID", Guid.NewGuid().ToString("n")));


        //    //TODO: in bayad bere jaei ke taghiri to Permission midim bere to Claim Update Kone
        //    var ctx = new ERPDbContext();
        //    var user = ctx.Users.Include(a => a.Permissions).Single(a => a.Id == _user.Id);
        //    var allUserRoleId = _user.Roles.Select(b => b.RoleId).ToList();
        //    var allUserRoles = ctx.Roles.Include(a => a.Permissions).Where(a => allUserRoleId.Contains(a.Id)).ToList();


        //    allUserRoles.ForEach(a => userIdentity.AddClaim(new Claim("BusinessRole", a.BusinessRoleId.ToString())));


        //    #region Add Permission To clame
        //    List<int> allPermission = new List<int>();
        //    allUserRoles.ForEach(a => allPermission.AddRange(a.Permissions.Select(b => b.Id)));
        //    allPermission.AddRange(user.Permissions.Select(a => a.Id));
        //    var permisions = allPermission.Distinct().ToList();
        //    permisions.ForEach(a =>
        //    userIdentity.AddClaim(new Claim("Permissions", a.ToString())));
        //    #endregion Add Permission To clame

        //    var inspectionNameItem = userIdentity.Claims.FirstOrDefault(a => a.Type == "InspectionName");
        //    var dispName = user.Name;
        //    if (inspectionNameItem != null)
        //        dispName += " - " + inspectionNameItem.Value;

        //    userIdentity.AddClaim(new Claim("DisplayName", dispName));

        //    // Add custom user claims here
        //    return userIdentity;
        //}



    }
}