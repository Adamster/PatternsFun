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
            var pilot = PilotFactory.CreateNewPilot("TestPilot", DateTime.Parse("24/04/1999"), 22, "team");

            PilotRepository.AddPilot(pilot);

            var car = MaranelloCarFactory.CreateNewCar(100, 1000, 1000, EngineTypes.V12, "Ferrari", null);
            PilotRepository.AddCar(pilot,car);



            Console.ReadLine();
            log.SaveToFile();
        }
    }
}