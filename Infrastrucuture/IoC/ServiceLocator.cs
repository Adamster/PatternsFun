// PatternsFun by Serghei Adam 
// Created 29 07 2015 
// Edited 03 08 2015

using ActionImplementation;
using InterfacesActions;
using Ninject;
using Repository.Interfaces;
using Repository;

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
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}