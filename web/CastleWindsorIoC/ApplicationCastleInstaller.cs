using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using ActionImplementation;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using InterfacesActions;
using Repository;
using Repository.Interfaces;

namespace Web.CastleWindsorIoC
{
    public class ApplicationCastleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(
                Component.For(typeof (IRepository))
                    .ImplementedBy(typeof (Repository.Repository))
                    .LifestylePerWebRequest());
            container.Register(
                Component.For(typeof (IPilotRepository))
                    .ImplementedBy(typeof (PilotRepository))
                    .LifestylePerWebRequest());

            container.Register(
                Component.For(typeof (ICarRepository))
                    .ImplementedBy(typeof (CarRepository))
                    .LifestylePerWebRequest());

            container.Register(
                Component.For(typeof (ICarActionOnCreation))
                    .ImplementedBy(typeof (FillTank))
                    .LifestylePerWebRequest());

            container.Register(
                Component.For(typeof (IElectroCarActionOnCreation))
                    .ImplementedBy(typeof (ElectroActions))
                    .LifestylePerWebRequest());


            var contollers =
                Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof (Controller)).ToList();

            foreach (var controller in contollers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }
        }
    }
}