using System;
using Domain.EnginesTypes;
using Factories;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Infrastrucuture.IoC;
using Repository;
using Repository.Interfaces;
using Utils;

namespace Presentation
{
    internal class Program
    {
        private static readonly CarFactory MaranelloCarFactory;
        private static ICarRepository CarRepository;
        private static readonly IPilotRepository PilotRepository;

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

            #region comments

            //DbCreateService.CreateDbStrucutre();           
            // DbCreateService.CreateCustomTable();
            // DbAdapterService.Adapter();
            // DbCreateService.ScalarTest();
            //  DbCreateService.ReaderTest();
            //DbCreateService.ParametrQuery(30);
            //  DbAdapterService.Adapter();
            // CSharpDemo.GetAFunc();

            #endregion

            var pilot = PilotFactory.CreateNewPilot("Richman", "20/03/1999", 124, "TestTeam");

            var ferrari = MaranelloCarFactory.CreateNewCar(100, 100, 100, EngineTypes.V10, "TestCar", null, pilot);
            var ferrari2 = MaranelloCarFactory.CreateNewCar(100, 100, 100, EngineTypes.V10, "TestCar2", null, pilot);

            pilot.AddCar(ferrari);
            pilot.AddCar(ferrari2);

            PilotRepository.AddPilot(pilot);
            Console.ReadLine();
            log.SaveToFile();
        }
    }
}