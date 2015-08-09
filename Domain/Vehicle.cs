// File: Vehicle.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 07 08 2015

using System;
using Utils;

namespace Domain
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

        public void RemoveWeight(double weightValue)
        {
            if (Weight - weightValue > 0)
                Weight -= weightValue;
            else Console.WriteLine("Weight can't be zero or below");
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