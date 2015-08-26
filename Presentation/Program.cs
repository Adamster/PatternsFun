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
            var log = Logger.GetLogger();
            MaranelloCarFactory = ServiceLocator.Get<CarFactory>();
            CarRepository = ServiceLocator.Get<CarRepository>();
            PilotRepository = ServiceLocator.Get<PilotRepository>();

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

            var ferrari = MaranelloCarFactory.CreateNewCar(100, 100, 600, EngineTypes.V10, "TestCar", null, pilot);
            var ferrari2 = MaranelloCarFactory.CreateNewSportCar(100, 600, 100, EngineTypes.V10, "TestSportCar2", null,
                pilot);


          //CarRepository.Save(ferrari2);
           pilot.AddCar(ferrari);
            pilot.AddCar(ferrari2);

           PilotRepository.AddPilot(pilot);

            var electroPilot = PilotFactory.CreateNewPilot("ElectroPilot", "20/03/2014", 24, "Venturi");
            var tesla = MaranelloCarFactory.CreateNewElectroCar("Tesla", 1500, 452, electroPilot);
         //   electroPilot.AddCar(tesla);
            //PilotRepository.UpdatePilotAge(101, 244);
           // PilotRepository.DeletePilot(2121);
           PilotRepository.AddPilot(electroPilot);
            Console.ReadLine();
            log.SaveToFile();
        }
    }
}