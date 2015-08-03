// PatternsFun by Serghei Adam 
// Created 29 07 2015 
// Edited 03 08 2015

using System;
using Domain.Utils;

namespace Domain.Domain
{
    public abstract class Vehicle
    {
        public string Name { get; set; }
        public double? Mileage { get; set; }
        protected double Speed { get; set; }
        protected double Weight { get; set; }
        public string SpecialAdds { get; protected set; }
        protected double AccelerationSpeed { get; set; }
        public abstract void Accelerate(int toSpeed);
        public abstract void Brake();

        public void PrintCurrentSpeed()
        {
            Console.WriteLine(Name + " Speed: " + Speed.ToString("F2") + " km/h");
            Logger.AddMsgToLog(Name + " Speed: " + Speed.ToString("F2") + " km/h");
           
        }

        public double GetSpeed()
        {
            return Speed;
        }

        public double GetWeight()
        {
            return Weight;
        }

        public void ShowVehicleState()
        {
            if (Mileage != null)
                Console.WriteLine("\nThis {0} isn't new, " + Mileage.Value.ToString("f3") + "m traveled\n", Name);
            else Console.WriteLine("\nThis {0} is new!\n", Name);
        }
    }

    public interface IParams
    {
        IParams WithParams(Func<string> paramsDelegate);
        string GetParams();
    }
}