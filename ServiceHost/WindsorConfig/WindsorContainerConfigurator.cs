using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FluentValidation.WebApi;
using LuceneSearch.Core;
using Owin;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace ServiceHost.WindsorConfig
{
    public class WindsorContainerConfigurator
    {
        public IWindsorContainer Configure(IAppBuilder app)
        {
            var container = new WindsorContainer();

            container.Register(Component.For<IAppBuilder>().Instance(app));

            container.Install(FromAssembly.This(new WindsorBootstrap()))
                    .Install(FromAssembly.Containing<Framework.Config.Installers.ExceptionTranslatorInstaller>())   //install Framwork 
                    .Install(FromAssembly.Containing<SearchEngine.Config.Installers.ApiControllerInstaller>(new WindsorBootstrap()));

            CreateDbIndex x=container.Resolve<CreateDbIndex>();
            x.Start(HttpRuntime.AppDomainAppPath + @"App_Data\idx");


            FluentValidationModelValidatorProvider
                .Configure(GlobalConfiguration.Configuration, provider => provider.ValidatorFactory = new WindsorValidatorFactory(container));

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));
            //config.DependencyResolver = new WindsorResolver(container);
            return container;
        }
    }
}