using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Facilities.Logging;
using Castle.Services.Logging.NLogIntegration;
using System.Web.Mvc;
using ServiceHost.Controllers;
using Framework.Persistance.EF;
using Framework.Core;
using Common.Infrastructure.Persistance.EF;

namespace ServiceHost.WindsorConfig
{
    public class ContextInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(Classes
                                  .FromAssemblyContaining<CommonDbContext>()
                                  .BasedOn<IUnitOfWork>()
                                  .WithService.Base()
                                  .LifestyleBoundToNearest<IFacadeService>());

            //container.Register(Component
            //                      .For<CommonDbContext>()
            //                      .LifestyleTransient());
        }

       
    }
}