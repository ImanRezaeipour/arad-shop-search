using Microsoft.Owin.Security.OAuth;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ServiceHost
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            //filters.Add(new ValidateModelAttribute());
        }
    }
}
