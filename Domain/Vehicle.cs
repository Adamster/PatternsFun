﻿using System;
using System.Collections.Generic;
using Utils;

namespace Domain
{
    public  class Vehicle : Entity
    {
        public virtual string Name { get; set; }
        public virtual double? Mileage { get; set; }
        protected virtual double Speed { get; set; }
        protected virtual double Weight { get; set; }
        public virtual string SpecialAdds { get; protected set; }
        protected virtual double AccelerationSpeed { get;  set; }
        public virtual void Accelerate(int toSpeed) { }
        public virtual void Brake() { }

        public virtual void PrintCurrentSpeed()
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


        public virtual double GetSpeed()
        {
            return Speed;
        }

        public virtual double GetWeight()
        {
            return Weight;
        }

        public virtual void RemoveWeight(double weightValue)
        {
            if (Weight - weightValue > 0)
                Weight -= weightValue;
            else Console.WriteLine("Weight can't be zero or below");
        }

        public virtual void ShowVehicleState()
        {
            if (Mileage != null)
                Console.WriteLine("\nThis {0} isn't new, " + Mileage.Value.ToString("f3") + "m traveled\n", Name);
            else Console.WriteLine("\nThis {0} is new!\n", Name);
        }
    }

}