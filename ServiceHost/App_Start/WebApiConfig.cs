using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace ServiceHost
{
    public static class WebApiConfig
    {
        #region =================== Public static Property =======================

        public static IAppBuilder App { get; set; }

        #endregion =================== Public Property =======================

        #region =================== Public static Methods ========================

        public static void Register(HttpConfiguration config)
        {

            config.SuppressDefaultHostAuthentication();

            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            //config.Filters.Add(new ValidateModelAttribute());
            //config.Filters.Add(new NotImplExceptionFilterAttribute());

            //config.Services.Add(typeof(IExceptionLogger), new TraceExceptionLogger());
            //config.Services.Replace(typeof(IExceptionHandler), new OopsExceptionHandler());
            
            App.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //ODataModelBuilder builder = new ODataConventionModelBuilder();
          
            //ODataRegister.Register(builder, config);
           
            //config.MapODataServiceRoute(
            //    routeName: "ODataRoute",
            //    routePrefix: "OApi",
            //    model: builder.GetEdmModel());


        }

        #endregion =================== Public Methods ========================
    }
}