using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.CastleWindsorFacility;
using SearchEngine.WebAPI.Controllers;

namespace SearchEngine.Config.Installers
{
    [InstallerPriority(4)]
    public class ApiControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
            Classes
                .FromAssemblyContaining<SearchEngineController>()
                .BasedOn<ApiController>()
                .LifestylePerWebRequest()
            );
        }
    }
}
