// File: ServiceLocator.cs in
// PatternsFun by Serghei Adam 
// Created 21 08 2015 
// Edited 21 08 2015

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
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}