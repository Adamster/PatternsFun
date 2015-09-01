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
            //DbCreateService.CreateCustomTable();
            //DbAdapterService.Adapter();
            //DbCreateService.ScalarTest();
            //DbCreateService.ReaderTest();
            //DbCreateService.ParametrQuery(30);
            //DbAdapterService.Adapter();
            // CSharpDemo.GetAFunc();

            #endregion

            //  GenerateData();
            // TestDb();
           // ShowVehicleName();

            Console.WriteLine("press any key to exit...");
            Console.ReadLine();
            log.SaveToFile();
        }

        private static void GenerateData()
        {
            var pilot = PilotFactory.CreateNewPilot("Pilot W/O Cars", "01/09/2015", 19, "TestTeam #1");
            PilotRepository.AddPilot(pilot);

            var pilot2 = PilotFactory.CreateNewPilot("Pilot With 3 Cars", "01/09/2015", 19, "TestTeam #2");
            var car1 = MaranelloCarFactory.CreateNewCar(100, 2100, 900, EngineTypes.V10, "Ferrari 900", null, pilot2);
            var car2 = MaranelloCarFactory.CreateNewCar(100, 2100, 800, EngineTypes.V10, "Ferrari 800", null, pilot2);
            var car3 = MaranelloCarFactory.CreateNewCar(100, 2100, 700, EngineTypes.V10, "Ferrari 700", null, pilot2);
            pilot2.AddCar(car1);
            pilot2.AddCar(car2);
            pilot2.AddCar(car3);
            PilotRepository.AddPilot(pilot2);

        }

        private static void ShowVehicleName()
        {
            var vehicleName = PilotRepository.GetVehicleName();
            if (vehicleName != null)
            {
                foreach (var vehicle in vehicleName)
                {
                    Console.WriteLine(vehicle.Name);
                }
            }
        }

        private static void TestDb()
        {
            var pilot = PilotFactory.CreateNewPilot("Richman", "20/03/1999", 124, "TestTeam");

            var ferrari = MaranelloCarFactory.CreateNewCar(100, 100, 600, EngineTypes.V10, "TestCar", null, pilot);
            var ferrari2 = MaranelloCarFactory.CreateNewSportCar(100, 600, 100, EngineTypes.V10, "TestSportCar2", null,
                pilot);


            pilot.AddCar(ferrari);
            pilot.AddCar(ferrari2);

            PilotRepository.AddPilot(pilot);

            var electroPilot = PilotFactory.CreateNewPilot("ElectroPilot", "20/03/2014", 24, "Venturi");
            var tesla = MaranelloCarFactory.CreateNewElectroCar("Tesla", 1500, 452, electroPilot);
            electroPilot.AddCar(tesla);
            PilotRepository.UpdatePilotAge(101, 244);
            // PilotRepository.DeletePilot(2121);
            PilotRepository.AddPilot(electroPilot);
        }
    }
}