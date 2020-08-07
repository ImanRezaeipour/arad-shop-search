using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.CastleWindsorFacility;

namespace SearchEngine.Config.Installers
{
    [InstallerPriority(6)]
    public class ConfigurationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(Component
            //        .For<ITicketingConfiguration>()
            //        .UsingFactoryMethod(kernel => TicketingConfiguration.Config)
            //        .LifestyleSingleton());
        }
    }
}
