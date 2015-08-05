// File: ServiceLocator.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 05 08 2015

using ActionImplementation;
using InterfacesActions;
using Ninject;

namespace Infrastrucuture.IoC
{
    internal static class ServiceLocator
    {
        private static readonly IKernel Kernel = new StandardKernel();

        public static void RegisterAll()
        {
            Kernel.Bind<ICarActionOnCreation>()
                .To<FillTank>();
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}