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
        private static CarFactory MaranelloCarFactory;
        private static ICarRepository CarRepository;
        private static IPilotRepository PilotRepository;


        static Program()
        {
            ServiceLocator.RegisterAll();
           
            NHibernateProfiler.Initialize();
        }

        private static void Main(string[] args)
        {
            MaranelloCarFactory = ServiceLocator.Get<CarFactory>();
            CarRepository = ServiceLocator.Get<CarRepository>();
            PilotRepository = ServiceLocator.Get<PilotRepository>();

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
            var ferrari = MaranelloCarFactory.CreateNewCar(100, 100, 100, EngineTypes.V10, "TestCar", null, "TestPilot",  "24/02/1999", 24, "TestTeam");
            var ferrari2 = MaranelloCarFactory.CreateNewCar(100, 100, 100, EngineTypes.V10, "TestCar2", null, "TestPilot",  "24/02/1999", 24, "TestTeam");



            pilot.AddCar(ferrari);
            pilot.AddCar(ferrari2);


            //CarRepository.Save(ferrari);
            //CarRepository.Save(ferrari2);
            PilotRepository.AddPilot(pilot);

          //  PilotRepository.UpdatePilotAge(2, 19);

            Console.ReadLine();
            log.SaveToFile();
        }
    }
}