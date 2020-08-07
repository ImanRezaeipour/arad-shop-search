using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.CastleWindsorFacility;
using Framework.Core;
using SearchEngine.Application;

namespace SearchEngine.Config.Installers
{
    [InstallerPriority(1)]
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes
                              .FromAssemblyContaining<SearchService>()
                              .BasedOn<IService>()
                              .WithService.FromInterface()
                              .LifestyleBoundToNearest<IFacadeService>());

           
        }
    }
}
