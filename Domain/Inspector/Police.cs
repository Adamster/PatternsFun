// File: Police.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 07 08 2015

#region

using System;
using System.Collections.Generic;
using System.Threading;
using Utils;

#endregion

namespace Domain.Inspector
{
    public class Police
    {
        private static readonly Police InstancePolice = new Police();
        private readonly List<Vehicle> penaltyParking;
        private TimeSpan TimeArrested = TimeSpan.FromDays(7);

        static Police()
        {
        }

        private Police()
        {
            penaltyParking = new List<Vehicle>();
        }

        public static Police Instance
        {
            get { return InstancePolice; }
        }

        public void ChaseTheCar(Vehicle suspectVehicle)
        {
            TurnOnSirens();
            StopSuspectCar(suspectVehicle);
        }

        private void StopSuspectCar(Vehicle suspectVehicle)
        {
            Console.WriteLine("Pull over your vehicle! NOW!");
            TurnOnSirens();
            while (suspectVehicle.GetSpeed() != 0)
            {
                suspectVehicle.Brake();
                TurnOnSirens();
            }
            if (suspectVehicle.GetSpeed() == 0)
                AddSuspect(suspectVehicle);
        }

        private void AddSuspect(Vehicle suspectVehicle)
        {
            penaltyParking.Add(suspectVehicle);
            Logger.AddMsgToLog("Suspect arrested: " + suspectVehicle.Name);
        }

        private void TurnOnSirens()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("loud sounds");
            Thread.Sleep(150);

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("loud sounds");
        }

        public void PrintSuspects()
        {
            Console.WriteLine("\nVehicles on penaltyParking:\n");
            Console.ResetColor();
            var i = 0;

            foreach (var vehicle in penaltyParking)
            {
                i++;
                Console.WriteLine(i + ": " + vehicle.Name + " for " + TimeArrested.Days + " Days");
            }
        }
    }
}