// File: Program.cs in
// PatternsFun by Serghei Adam 
// Created 27 07 2015 
// Edited 04 08 2015

#region $access$

using System;
using System.Threading;
using Domain.Domain.Engines;
using Domain.Domain.Inspector;
using Domain.Utils;
using Factories;
using Infrastrucuture.IoC;

#endregion

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

        private static void Main(string[] args)
        {
            Logger log = Logger.GetLogger();
            CarFactoryTestAndOthers();


            Console.ReadLine();
            log.SaveToFile();
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