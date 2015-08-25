using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Domain;
using Domain.CarTypes;
using Domain.EnginesTypes;
using Domain.FuelTypes;
using Domain.Inspector;
using Domain.Interfaces;
using Domain.Paddock;
using Domain.Patterns.Decorator;
using Domain.Patterns.Observer;
using Domain.Patterns.Proxy;
using Domain.Patterns.Visitor;
using Domain.Persons;
using Factories;
using Utils;

namespace PatternsFun
{
    internal class CSharpDemo
    {
        public static Func<int, int> GetAFunc()
        {
            var myVar = 1;
            Func<int, int> inc = delegate(int var1)
            {
                myVar = myVar + 1;
                return var1 + myVar;
            };
            return inc;
        }

        private static void CSharpFeatures()
        {
            var cars = CreateNumberOfCars("TestCars");
            var carsCustom = CreateNumberOfCars("Prototype", 50);
            var carsCustom2 = CreateNumberOfCars("Car", hpPower: (int) 450.50, count: 15, weight: 1234);
            cars[1] = new SportCar(100, 1500, new GasolineEngine(150, EngineTypes.V2), "implicitCastSportCar", null);

            var testCar = cars[1] as SportCar;

            var testList = cars[1] + cars[2];

            Console.WriteLine((object) ~cars[4]);

            Console.WriteLine(cars[2] is SportCar);
            Console.WriteLine(cars[2] is Vehicle);
        }

        private static List<Car> CreateNumberOfCars(string name, int count = 10, int hpPower = 500, int weight = 1500)
        {
            List<Car> freshCars = new List<Car>();

            for (int j = 0; j < count; j++)
            {
                freshCars.Add(new Car(0, weight, new GasolineEngine(hpPower, EngineTypes.V2), name + " #" + j, null));
            }

            return freshCars;
        }

        private static void VisitTest()
        {
            var artefactCollector = new ArtefactCollectorVisitor();

            PitLane monacoPitLane = new PitLane(500, "Monaco PitLane");

            Program.BoxList[1].Accept(artefactCollector);
            monacoPitLane.Accept(artefactCollector);

            foreach (var artefact in artefactCollector.ArtefactList)
            {
                Console.WriteLine("artefact: {0}", artefact);
            }
            Console.WriteLine("press enter to continue");
            Console.ReadLine();
        }

        private static void TemplateMethodTest()
        {
            Car ferrari = Program.MaranelloCarFactory.CreateNewSportCar(0, 1500, 500, EngineTypes.V6, "Ferrari 14 T",
                color => color.WithParams(() => "Color is Red"));
            Logger.AddMsgToLog("Ferrari 14 T created");

            Car prototypeCar = Program.MaranelloCarFactory.CreateNewCar(0, 1200, 250, EngineTypes.V4, "PrototypeCar", null);
            ferrari.ChangeOilRequest();
            prototypeCar.ChangeOilRequest();
        }

        private static void StrategyDemonstration()
        {
            var ferrari = Program.MaranelloCarFactory.CreateNewSportCar(0, 1500, 500, EngineTypes.V6, "Ferrari 14 T",
                color => color.WithParams(() => "Color is Red"));
            Logger.AddMsgToLog("Ferrari 14 T created");
            ferrari.Accelerate(350);

            var ferrari2 = Program.MaranelloCarFactory.CreateNewSportCar(0, 1500, 500, EngineTypes.V6, "Ferrari 14 T(Diesel)",
                color => color.WithParams(() => "Color is Red"));

            ferrari2.SetFuelType(new Diesel());
            ferrari2.Accelerate(350);
        }

        private static void ObserverTest()
        {
            var charlieWhiting = new RaceDirector();
            var but = PilotFactory.CreateNewPilot("Jenson Button", DateTime.Parse("12/03/2000"), 35, "McLaren F1");
            var msc = PilotFactory.CreateNewPilot("Michael Schumaher", DateTime.Parse("25/08/1991"), 46, "Ferrari F1");
            charlieWhiting.JoinRace(but);
            charlieWhiting.JoinRace(msc);

            charlieWhiting.RaceStatus = "Race Started!";
            charlieWhiting.RaceStatus = "Safety Car";
            charlieWhiting.RaceStatus = "Red Flag";
            charlieWhiting.RaceStatus = "Restart";


            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
        }

        private static void ProxyTest()
        {
            var but = PilotFactory.CreateNewPilot("Jenson Button", DateTime.Parse("12/03/2000"), 35, "McLaren F1");
            var msc = PilotFactory.CreateNewPilot("Michael Schumaher", DateTime.Parse("25/08/1991"), 46, "Ferrari F1");
            Console.WriteLine("{0} has {1} Days experience", but.Name, but.ExpierenceTime.Days);
            Console.WriteLine("{0} has {1} Days experience", msc.Name, msc.ExpierenceTime.Days);

            var richFan = new Fan("John Doe", PaddockAccessLevels.ClubPaddock);
            var simpleFan = new Fan("Joan Doe", PaddockAccessLevels.Gold);
            var boxes = new BoxesProxy(richFan, Program.BoxList.First());
            var boxes1 = new BoxesProxy(simpleFan, Program.BoxList[7]);
            var pilotBoxes = new BoxesProxy(but, Program.BoxList[7]);
            pilotBoxes.PilotAcces();
            boxes.GrantAcces();
            boxes1.GrantAcces();
            Console.WriteLine("press enter to continue");
        }

        private static void DecoratorTune()
        {
            var ferrari = Program.MaranelloCarFactory.CreateNewSportCar(0, 1500, 500, EngineTypes.V6, "Ferrari 14 T",
                color => color.WithParams(() => "Color is Red"));

            Console.WriteLine("\nStock version");
            Console.WriteLine("---------------");
            ferrari.Accelerate(100);
            ferrari.StopTheCar();

            Console.WriteLine("\nEngine tuned version");
            Console.WriteLine("---------------");
            IVehicleComponent engineTune = new TuneEngine(ferrari);
            engineTune.TunePart();
            ferrari.Accelerate(100);
            ferrari.StopTheCar();


            Console.WriteLine("\nWheelsTune version");
            Console.WriteLine("---------------");
            IVehicleComponent wheelComponent = new TuneWheels(ferrari);
            wheelComponent.TunePart();
            ferrari.Accelerate(100);
        }

        private static void CarFactoryTestAndOthers()
        {
            Console.Write(new string('▒', 320));
            Console.Write(new string('█', 320));
            Console.Write(new string('▒', 320));

            Logger.AddMsgToLog("Program launched");

            var ferrari = Program.MaranelloCarFactory.CreateNewSportCar(0, 1500, 1000, EngineTypes.V6, "Ferrari 14 T",
                color => color.WithParams(() => "Color is Red"));
            Logger.AddMsgToLog("Ferrari 14 T created");

            var monster = Program.MaranelloCarFactory.CreateNewCar(100, 2000, 1100, EngineTypes.V16, "Ferrari LaFerrari",
                param => param.WithParams(() => "Carbon Brakes"));
            Logger.AddMsgToLog("Ferrari LaFerrari created");

            Thread.Sleep(2000);

            var simpleCar = Program.MaranelloCarFactory.CreateNewCar(0, 200, 200, EngineTypes.V2, "Ferrari 458", null);
            Logger.AddMsgToLog("Ferrari 458 created");
            monster.Accelerate(10);


            ferrari.Accelerate(300);


            var police = Police.Instance;
            police.ChaseTheCar(ferrari);
            police.ChaseTheCar(monster);
            police.PrintSuspects();
            Console.WriteLine("press enter to continue");
            Console.ReadLine();
        }
    }
}