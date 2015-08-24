using System;
using Domain;
using Domain.Persons;
using Domain.Utils;
using Factories;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Infrastrucuture.IoC;
using Repository;
using Repository.Interfaces;

namespace Presentation
{
    internal class Program
    {
        private static CarFactory MaranelloCarFactory;
        private static ICarRepository CarRepository;
        private static IPilotRepository PilotRepository;

        static Program()
        {
            ServiceLocator.RegisterAll();
            MaranelloCarFactory = ServiceLocator.Get<CarFactory>();
            CarRepository = ServiceLocator.Get<CarRepository>();
            PilotRepository = ServiceLocator.Get<PilotRepository>();
            NHibernateProfiler.Initialize();
        }

        private static void Main(string[] args)
        {

            var log = Logger.GetLogger();

            var ferrari = new Vehicle("Ferrari created", 0, 123, 12345, "Carbon brakes", 10);
            var pilot = new Pilot("Jenson Button", DateTime.Parse("12/03/2000"), 35, "McLaren");


            Console.WriteLine("pilot created");
            pilot.CarVehicles.Add(ferrari);

            PilotRepository.AddPilot(pilot);


            var buggati = new Vehicle("Buggati", 0, 0, 123123, "Mountaint weight", 0);
            pilot.CarVehicles.Add(buggati);

            PilotRepository.AddPilot(pilot);


            Console.ReadLine();
            log.SaveToFile();
        }

        private static void Car(int number)
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