using Castle.Windsor.Installer;
using Framework.CastleWindsorFacility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceHost.WindsorConfig
{
    public class WindsorBootstrap : InstallerFactory
    {

        public override IEnumerable<Type> Select(IEnumerable<Type> installerTypes)
        {
            var retval = installerTypes.OrderBy(x => this.GetPriority(x));
            return retval;
        }

        private int GetPriority(Type type)
        {
            var attribute = type.GetCustomAttributes(typeof(InstallerPriorityAttribute), false).FirstOrDefault() as InstallerPriorityAttribute;
            return attribute != null ? attribute.Priority : InstallerPriorityAttribute.DefaultPriority;
        }

    }
}