using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Framework.AccountsManagementIdentity.Authorizations
{
    public class MultiClaimsAuthorizationAttribute : AuthorizationFilterAttribute
    {
        public string ClaimType { get; set; }
        public List<string> ClaimValue { get; set; }

        public MultiClaimsAuthorizationAttribute(params object[] claimValue)
        {
            ClaimType = "Permissions";
            ClaimValue = claimValue.Select(a => (Convert.ToInt32(a)).ToString()).ToList();
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
            
        //    //if (!ar.IsExist(Helper.GetHash(principal.Claims.First(a => a.Type == "SessionID").Value)))
        //    //{
        //    //    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        //    //    return Task.FromResult<object>(null);
        //    //} 
        //    #endregion

        //    if ((principal.HasClaim(x => x.Type == "BusinessRole" && x.Value.Split(',').Any(c => c=="11"))))
        //    {
        //        return Task.FromResult<object>(null);
        //    }
        //    if (!(principal.HasClaim(x => x.Type == ClaimType && x.Value.Split(',').Any(c => ClaimValue.Contains(c)))))
        //    {
        //        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        //        return Task.FromResult<object>(null);
        //    }

        //    //User is Authorized, complete execution
        //    return Task.FromResult<object>(null);
        //}
    }
}
