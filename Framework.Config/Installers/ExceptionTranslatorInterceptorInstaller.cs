using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.Core.ExceptionHandling;
using Framework.Decorator;

namespace Framework.Config.Installers
{
    public class ExceptionTranslatorInterceptorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ExceptionTranslatorInterceptor>()
                                        .LifestylePerWebRequest());
        }
    }
}
