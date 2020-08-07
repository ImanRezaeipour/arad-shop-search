using Framework.Core.Helper;
using Framework.Persistance.EF;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Framework.AccountsManagementIdentity.Authorizations
{
    public class ClaimsAuthorizationAttribute : AuthorizationFilterAttribute
    {
        public string ClaimType { get; set; }
        public int ClaimValue { get; set; }

        public ClaimsAuthorizationAttribute(object claimValue)
        {
            ClaimType = "Permissions";
            ClaimValue = (int)claimValue;
        }

        //public override Task OnAuthorizationAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        //{
        //    var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

        //    if (!principal.Identity.IsAuthenticated)
        //    {
        //        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        //        return Task.FromResult<object>(null);
        //    }

        //    #region Check DB
        //    var ar = new AuthRepository();
        //    if (!ar.IsExist(CustomHash.GetHash(principal.Claims.First(a => a.Type == "SessionID").Value)))
        //    {
        //        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        //        return Task.FromResult<object>(null);
        //    } 
        //    #endregion

        //    if (!(principal.HasClaim(x => x.Type == ClaimType && x.Value.Split(',').Contains(ClaimValue.ToString()))))
        //    {
        //        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        //        return Task.FromResult<object>(null);
        //    }

        //    //User is Authorized, complete execution
        //    return Task.FromResult<object>(null);
        //}
    }
}