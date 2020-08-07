using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.CastleWindsorFacility;

namespace SearchEngine.Config.Installers
{
    [InstallerPriority(5)]
    public class OdataApiControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container
            //    .Register(
            //       Classes
            //           .FromAssemblyContaining<OdataOrderController>()
            //           .BasedOn<ODataController>()
            //           .LifestylePerWebRequest()
            //       );
        }
    }
}
