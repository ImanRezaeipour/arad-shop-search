using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.CastleWindsorFacility;
using Framework.Core;
using Framework.Decorator;
using SearchEngine.Interface.Facade;

namespace SearchEngine.Config.Installers
{
    [InstallerPriority(2)]
    public class FacadeInstaller : IWindsorInstaller

    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes
                              .FromAssemblyContaining<SearchFacade>()
                              .BasedOn<IFacadeService>()
                              .WithService.FromInterface()
                              .LifestyleBoundToNearest<IGateway>()
                              .Configure(a => a.Interceptors<ExceptionTranslatorInterceptor>()));
        }
    }
}
