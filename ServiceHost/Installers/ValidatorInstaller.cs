using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Common.Infrastructure.Persistance.EF.Validation;
using FluentValidation;

namespace ServiceHost.WindsorConfig
{
    public class ValidatorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                 Classes.FromAssemblyContaining<ProductValidator>()
                 .BasedOn(typeof(AbstractValidator<>))
                 .WithServiceFirstInterface()
                 .LifestylePerWebRequest());

        }
    }
}