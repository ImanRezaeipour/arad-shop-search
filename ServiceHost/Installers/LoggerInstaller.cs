using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Facilities.Logging;
using Castle.Services.Logging.NLogIntegration;

namespace ServiceHost.WindsorConfig
{
    public class LoggerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<LoggingFacility>(m => m.LogUsing<NLogFactory>().WithConfig("NLog.config"));
        }

       
    }
}