// File: Vehicle.cs in
// PatternsFun by Serghei Adam 
// Created 21 08 2015 
// Edited 21 08 2015

using System;
using Domain.Utils;

namespace Domain.Domain
{
    public class Vehicle : Entity
    {
        public Vehicle(string name, double? mileage, double speed, double weight, string specialAdds,
            double accelerationSpeed)
        {
            Name = name;
            Mileage = mileage;
            Speed = speed;
            Weight = weight;
            SpecialAdds = specialAdds;
            AccelerationSpeed = accelerationSpeed;
        }

        [Obsolete]
        protected Vehicle()
        {
        }

        public virtual string Name { get; protected set; }
        public virtual double? Mileage { get; protected set; }
        protected virtual double Speed { get; set; }
        protected virtual double Weight { get; set; }
        public virtual string SpecialAdds { get; protected set; }
        protected virtual double AccelerationSpeed { get; set; }

        public virtual void Accelerate(int toSpeed)
        {
        }

        public virtual void Brake()
        {
        }

        public virtual void PrintCurrentSpeed()
        {
            Console.WriteLine(Name + " Speed: " + Speed.ToString("F2") + " km/h");
            Logger.AddMsgToLog(Name + " Speed: " + Speed.ToString("F2") + " km/h");
        }

        public virtual double GetSpeed()
        {
            return Speed;
        }

        public virtual double GetWeight()
        {
            return Weight;
        }

        public virtual void ShowVehicleState()
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