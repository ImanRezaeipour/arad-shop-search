using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.CastleWindsorFacility;

namespace SearchEngine.Config.Installers
{
    [InstallerPriority(0)]
    public class AutoMapperProfilesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
             Classes
                 .FromThisAssembly()
                 .BasedOn<Profile>()
                 .WithService.Base()
                 .Configure(c => c.Named(c.Implementation.FullName))
                 .LifestyleSingleton()
             );
        }
    }
}
