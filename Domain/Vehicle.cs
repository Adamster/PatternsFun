// File: Vehicle.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 10 08 2015

using System;
using System.Collections.Generic;
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

        public static List<Vehicle> operator +(Vehicle x, Vehicle y)
        {
            List<Vehicle> list = new List<Vehicle>();

            list.Add(x);
            list.Add(y);

            return list;
        }

        public static bool operator <(Vehicle x, Vehicle y)
        {
            return x.GetWeight() < y.GetWeight();
        }

        public static bool operator >(Vehicle x, Vehicle y)
        {
            return x.GetWeight() > y.GetWeight();
        }

        public static Vehicle operator ~(Vehicle x)
        {
            Console.WriteLine("Name: {0}",x.Name);
            Console.WriteLine("Weight: {0}",x.Weight);
            Console.WriteLine("Speed: {0}", x.Speed);
            Console.WriteLine("acceleration speed: {0}", x.AccelerationSpeed);
            return x;
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