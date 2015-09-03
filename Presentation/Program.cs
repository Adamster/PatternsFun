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
        private static readonly ICarRepository CarRepository;
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
            // ShowPilotCarCountCrutch();
            //  ShowPilotCarCount();
            ShowUniquePilot();
            // ShowAvgHpPerPilot();

            //   getOldestPilot();
            //   getMTeamDrivers();
            //  GetClassification();
            // GetCarDetails();
            // GetCarDetailsPilot();

            Console.WriteLine("press any key to exit...");
            Console.ReadLine();
            log.SaveToFile();
        }

        private static void GetCarDetailsPilot()
        {
            var res = CarRepository.GetCarDetailsWithPilot();
            foreach (var carDetailsDto in res)
            {
                Console.WriteLine("Pilot: " + carDetailsDto.PilotName + "\n" + carDetailsDto.Name
                                  + " weight " + carDetailsDto.Weight +
                                  " tank volume " + carDetailsDto.TankVolume + "\n");
            }
        }

        private static void GetCarDetails()
        {
            var res = CarRepository.GetCarDetails();
            foreach (var carDetailsDto in res)
            {
                Console.WriteLine(carDetailsDto.Name
                                  + " hp " + carDetailsDto.HorsePowers + " weight " + carDetailsDto.Weight +
                                  " tank volume " + carDetailsDto.TankVolume);
            }
        }

        private static void GetClassification()
        {
            var res = PilotRepository.GetCarClassifciationByHp();
            Console.WriteLine("car types");
            foreach (var re in res)
            {
                Console.WriteLine(re);
            }
        }

        private static void GetMTeamDrivers()
        {
            var mDrivers = PilotRepository.GetMTeamDrivers();
            foreach (object[] objects in mDrivers)
            {
                Console.WriteLine(objects[0] + " " + objects[1]);
            }
        }

        private static void GetOldestPilot()
        {
            var res = PilotRepository.GetOldestPilot();
            Console.WriteLine(res.Name + " age:" + res.Age);
        }


        private static void ShowAvgHpPerPilot()
        {
            var res = PilotRepository.GetAvgHorsePowerPerPilot();
            foreach (object[] pilot in res)
            {
                Console.WriteLine(pilot[0] == null ? "null" : string.Format(pilot[1] + " has avg power " + pilot[0]));
            }
        }

        private static void ShowPilotCarCount()
        {
            var res = PilotRepository.GetCarCountPerPilot();
            foreach (var pilotDetailsDto in res)
            {
                Console.WriteLine("name: " + pilotDetailsDto.Name + " from team " + pilotDetailsDto.Team + " has " +
                                  pilotDetailsDto.CarCount + " cars");
            }
        }

        private static void ShowUniquePilot()
        {
            var i = 0;
            var res = PilotRepository.GetUniquePilots();
            foreach (var pilot in res)
            {
                i++;
                Console.WriteLine("{0}) {1} from {2} debuted at {3}\n age {4},\n exp {5} days ", i, pilot.Name,
                    pilot.Team, pilot.DebutDate.ToShortDateString(), pilot.Age, pilot.ExpierenceTime.TotalDays);
            }
        }

        private static void ShowPilotCarCountCrutch()
        {
            var res = PilotRepository.GetCarCountPerPilotCrutchVersion();
            foreach (object[] re in res)
            {
                Console.WriteLine(string.Format(re[0] + " has  " + re[1] + " cars"));
            }
        }

        private static void GenerateData()
        {
            //var pilot = PilotFactory.CreateNewPilot("John Doe", "01/09/2015", 19, "McLaren");
            //PilotRepository.AddPilot(pilot);

            //var pilot2 = PilotFactory.CreateNewPilot("Joan Doe 3 Cars", "01/09/2015", 19, "Mercedes");
            //var car1 = MaranelloCarFactory.CreateNewCar(100, 2100, 900, EngineTypes.V10, "Ferrari 900", null, pilot2);
            //var car2 = MaranelloCarFactory.CreateNewCar(100, 2100, 800, EngineTypes.V10, "Ferrari 800", null, pilot2);
            //var car3 = MaranelloCarFactory.CreateNewCar(100, 2100, 700, EngineTypes.V10, "Ferrari 700", null, pilot2);


            //pilot2.AddCar(car1);
            //pilot2.AddCar(car2);
            //pilot2.AddCar(car3);
            //PilotRepository.AddPilot(pilot2);

            var car1 = MaranelloCarFactory.CreateNewCar(100, 2100, 900, EngineTypes.V10, "Ferrari 900", null, null);
            var car2 = MaranelloCarFactory.CreateNewCar(100, 2100, 800, EngineTypes.V10, "Ferrari 800", null, null);
            var car3 = MaranelloCarFactory.CreateNewCar(100, 2100, 700, EngineTypes.V10, "Ferrari 700", null, null);

            CarRepository.Save(car1);
            CarRepository.Save(car2);
            CarRepository.Save(car3);
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