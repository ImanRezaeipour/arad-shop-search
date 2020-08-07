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

namespace ServiceHost.WindsorConfig
{
    public class MvcInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
           Classes
               .FromAssemblyContaining<HomeController>()
               .BasedOn<Controller>()
               .LifestylePerWebRequest()
           );
        }

       
    }
}