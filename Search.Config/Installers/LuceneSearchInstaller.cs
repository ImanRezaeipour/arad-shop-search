using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.CastleWindsorFacility;
using LuceneSearch.Core;

namespace SearchEngine.Config.Installers
{
    [InstallerPriority(6)]
    public class LuceneSearchInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                    .For<CreateIndex>()
                    .LifestyleSingleton());

            container.Register(Component
                    .For<CreateDbIndex>()
                    .LifestyleSingleton());
            container.Register(Component
                    .For<AutoComplete>()
                    .LifestyleSingleton());
        }
    }
}
