using System;
using Domain.Domain;
using Domain.Utils;
using Factories;
using Infrastrucuture.IoC;
using Repository;
using Repository.Interfaces;

namespace Presentation
{
    internal class Program
    {
        private static readonly CarFactory MaranelloCarFactory;
        private static ICarRepository CarRepository;

        static Program()
        {
            ServiceLocator.RegisterAll();
            MaranelloCarFactory = ServiceLocator.Get<CarFactory>();
        }

        private static void Main(string[] args)
        {
            CarRepository = ServiceLocator.Get<CarRepository>();
            var log = Logger.GetLogger();

            SomeMethod(5);

            Console.ReadLine();
            log.SaveToFile();
        }

        private static void SomeMethod(int number)
        {
            Logger.AddMsgToLog("Program launched");

            for (var i = 0; i < number; i++)
            {
                var ferrari = new Vehicle(string.Format("Ferrari #{0} created", i), 0, 123, 12345, string.Empty, 24);

                CarRepository.Save(ferrari);
                Console.WriteLine("Ferrari 14 T created");
                Logger.AddMsgToLog("Ferrari 14 T created");
            }
        }
    }
}