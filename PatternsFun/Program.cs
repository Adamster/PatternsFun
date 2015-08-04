// File: Program.cs in
// PatternsFun by Serghei Adam 
// Created 04 08 2015 
// Edited 04 08 2015

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Domain.Domain.Decorator;
using Domain.Domain.Engines;
using Domain.Domain.Inspector;
using Domain.Domain.Interfaces;
using Domain.Utils;
using Factories;
using Infrastrucuture.IoC;
using Domain.Domain.Paddock;
using Domain.Domain.Persons;
using  Domain.Domain.Proxy;

namespace PatternsFun
{
    internal class Program
    {
        private static readonly CarFactory MaranelloCarFactory;
        static Program()
        {
            ServiceLocator.RegisterAll();
            MaranelloCarFactory = ServiceLocator.Get<CarFactory>();
        }


        private static List<Boxes> GetBoxes()
        {
            return new List<Boxes>
            {
                new Boxes("Mercedes", 1),
                new Boxes("Ferrari", 2),
                new Boxes("Williams", 3),
                new Boxes("Red Bull Racing", 4),
                new Boxes("Force India", 5),
                new Boxes("Lotus", 6),
                new Boxes("Torro Rosso", 7),
                new Boxes("McLaren", 8),
                new Boxes("Sauber", 9),
                new Boxes("Marussia", 10)
            };
        }

        private static void Main(string[] args)
        {
            Logger log = Logger.GetLogger();
            // CarFactoryTestAndOthers();
            //DecoratorTune();

            var but = PilotFactory.CreateNewPilot("Jenson Button", DateTime.Parse("12/03/2000"), 35, "McLaren F1");
            var msc = PilotFactory.CreateNewPilot("Michael Schumaher", DateTime.Parse("25/08/1991"), 46, "Ferrari F1");
            Console.WriteLine(  msc.ExpierenceTime.Days+ " Days");
           
            var richFan = new Fan("John Doe", PaddockAccessLevels.ClubPaddock);
            var simpleFan = new Fan("Joahn Doe", PaddockAccessLevels.Gold);
            var boxes = new BoxesProxy(richFan, GetBoxes().First());
            var boxes1 = new BoxesProxy(simpleFan, GetBoxes()[8]);
            boxes.GrantAcces();
            boxes1.GrantAcces();

            Console.ReadLine();
            log.SaveToFile();
        }

        private static void DecoratorTune()
        {
            var ferrari = MaranelloCarFactory.CreateNewSportCar(0, 1500, 500, EngineTypes.V6, "Ferrari 14 T",
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
            //Console.Write(new string('▒', 320));
            //Console.Write(new string('█', 320));
            //Console.Write(new string('▒', 320));
            Logger.AddMsgToLog("Program launched");

            var ferrari = MaranelloCarFactory.CreateNewSportCar(0, 1500, 500, EngineTypes.V6, "Ferrari 14 T",
                color => color.WithParams(() => "Color is Red"));
            Logger.AddMsgToLog("Ferrari 14 T created");

            var monster = MaranelloCarFactory.CreateNewCar(100, 2000, 1100, EngineTypes.V16, "Ferrari LaFerrari",
                param => param.WithParams(() => "Carbon Brakes"));
            Logger.AddMsgToLog("Ferrari LaFerrari created");

            Thread.Sleep(2000);

            var simpleCar = MaranelloCarFactory.CreateNewCar(0, 200, 200, EngineTypes.V2, "Ferrari 458",
                opt => opt.WithParams(() => ""));
            Logger.AddMsgToLog("Ferrari 458 created");
            monster.Accelerate(10);


            ferrari.Accelerate(30);


            var police = Police.Instance;
            police.ChaseTheCar(ferrari);
            police.ChaseTheCar(monster);
            police.PrintSuspects();

            Console.ReadLine();
        }
    }
}