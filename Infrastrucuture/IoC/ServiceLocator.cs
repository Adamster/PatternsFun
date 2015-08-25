using ActionImplementation;
using InterfacesActions;
using Ninject;
using Repository;
using Repository.Interfaces;

namespace Infrastrucuture.IoC
{
    internal static class ServiceLocator
    {
        private static readonly IKernel Kernel = new StandardKernel();

        public static void RegisterAll()
        {
            Kernel.Bind<ICarActionOnCreation>()
                .To<FillTank>();
            Kernel.Bind<ICarRepository>()
                .To<CarRepository>();
            Kernel.Bind<IPilotRepository>()
                .To<PilotRepository>();
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}