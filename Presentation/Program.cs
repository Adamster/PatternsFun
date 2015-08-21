// File: Program.cs in
// PatternsFun by Serghei Adam 
// Created 21 08 2015 
// Edited 21 08 2015

#region

using System;
using Domain.Domain;
using Domain.Domain.Engines;
using Domain.Utils;
using Factories;
using Infrastrucuture.IoC;
using Repository;
using Repository.Interfaces;

#endregion

namespace Presentation
{
    internal class Program
    {
        private static readonly CarFactory MaranelloCarFactory;
        private static  ICarRepository CarRepository;
        static Program()
        {
            ServiceLocator.RegisterAll();
            MaranelloCarFactory = ServiceLocator.Get<CarFactory>();
          
        }

        private static void Main(string[] args)
        {
            CarRepository = ServiceLocator.Get<CarRepository>();
            Logger log = Logger.GetLogger();

            SomeMethod(5);

            Console.ReadLine();
            log.SaveToFile();
        }

        private static void SomeMethod(int number)
        {
            Logger.AddMsgToLog("Program launched");

            for (int i = 0; i < number; i++)
            {
                var ferrari = new Vehicle(string.Format("Ferrari #{0} created",i), 0, 123, 12345, String.Empty, 24);

                CarRepository.Save(ferrari);
                Console.WriteLine("Ferrari 14 T created");
                Logger.AddMsgToLog("Ferrari 14 T created");

            }

          

        }
    }
}