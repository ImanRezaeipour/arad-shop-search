using AutoMapper;
using Castle.Windsor;
using Framework.AccountsManagementIdentity;
using Framework.AccountsManagementIdentity.Providers;
using LuceneSearch.Core;
using Microsoft.Owin;
using Owin;
using ServiceHost.WindsorConfig;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

[assembly: OwinStartup(typeof(ServiceHost.Startup))]

namespace ServiceHost
{
    public partial class Startup
    {
        #region =================== Private Variable ======================

        private static IWindsorContainer container;

        #endregion =================== Private Variable ======================

        #region =================== Public Methods ========================

        public void Configuration(IAppBuilder app)
        {
            Framework.Core.ServerInfo.ServerPath = System.Web.HttpRuntime.AppDomainAppPath;
            container = new WindsorContainerConfigurator().Configure(app); // Castel Windsor initialize

            WebApiConfig.App = app;
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            #region Automapper Initialize

            Mapper.Initialize(cfg => { container.ResolveAll<Profile>().ToList().ForEach(cfg.AddProfile); });

            #endregion Automapper Initialize

            app.Use<CustomAuthenticationMiddleware>();
            ConfigAuth.Container = container;
            ConfigAuth.ConfigureOAuthTokenGeneration(app);
            ConfigAuth.ConfigureOAuthTokenConsumption(app);




        }

        #endregion =================== Public Methods ========================
    }
}