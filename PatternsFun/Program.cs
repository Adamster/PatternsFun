
using System;
using System.Collections.Generic;
using Domain.Paddock;
using Factories;
using Infrastrucuture.IoC;
using Utils;

namespace PatternsFun
{
    internal class Program
    {
        internal static readonly CarFactory MaranelloCarFactory;

        #region private

        internal static List<Boxes> BoxList = new List<Boxes>
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

        #endregion

        static Program()
        {
            ServiceLocator.RegisterAll();
            MaranelloCarFactory = ServiceLocator.Get<CarFactory>();
        }

        private static void Main(string[] args)
        {

            // CarFactoryTestAndOthers();
            // DecoratorTune();
            // ProxyTest();
            //ObserverTest();
            //StrategyDemonstration();
            // TemplateMethodTest();

            var log = Logger.GetLogger();

            //DbCreateService.CreateDbStrucutre();           
            // DbCreateService.CreateCustomTable();
            // DbAdapterService.Adapter();
            // DbCreateService.ScalarTest();
            //  DbCreateService.ReaderTest();
            //DbCreateService.ParametrQuery(30);
            //  DbAdapterService.Adapter();
            CSharpDemo.GetAFunc();
            Console.WriteLine("press enter to exit");
            Console.ReadLine();
            log.SaveToFile();
        }
    }
}