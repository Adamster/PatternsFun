// File: SportCar.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 05 08 2015

using System;
using System.Diagnostics;
using Domain.EnginesTypes;
using Utils;

namespace Domain.CarTypes
{
    public class SportCar : Car
    {
        public SportCar(int fuelTankValue, double weightValue, GasolineEngine carEngineValue, string nameValue,
            string addParam)
            : base(fuelTankValue, weightValue, carEngineValue, nameValue, addParam)
        {
            DownForcePressure = 0;
        }

        private int DownForcePressure { get; set; }

        private double GetAccelerationSpeed()
        {
            return (Engine.HorsePowers/Weight*100) + DownForcePressure;
        }

        public override void Accelerate(int toSpeed)
        {
            PressThrottle(toSpeed);
        }

        private void PressThrottle(int toSpeed)
        {
            Engine.Start();
            Mileage = 0;
            while (Speed < toSpeed)
            {
                try
                {
                    if (BurnFuel())
                    {
                        if (Speed == 0) Speed += GetAccelerationSpeed();
                        else Speed += GetAccelerationSpeed() - Speed/10;
                        Console.WriteLine("Car Accelerate");
                        PrintCurrentSpeed();
                        Mileage += Speed;
                        GeneretaDownForce();
                    }
                }
                catch (FuelException ex)
                {
                    Logger.AddMsgToLog(ex.Message);
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Fill the tank?(Y/N:)");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        Console.WriteLine("How much?");
                        var addFuelTmp = Console.ReadLine();
                        double addFuel = 0;
                        double.TryParse(addFuelTmp, out addFuel);
                        FillTank(addFuel);
                    }
                    else
                        break;
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Fuel remaining in tank: {0}", FuelTank);
        }

        public override void Brake()
        {
            PressBrakePedal();
        }

        private void GeneretaDownForce()
        {
            DownForcePressure += 1;
            Console.WriteLine("Generating downforce");
        }

        private bool BurnFuel()
        {
            if (FuelTank > 0)
            {
                Console.WriteLine("current consume rate = {0}", BurnFuelRate(_fuelType));
                FuelTank -= BurnFuelRate(_fuelType);
                Console.WriteLine("burning fuel...");
                Debug.WriteLine("Fuel Burn succesfully, remaining in tank: " + FuelTank);
                return true;
            }
            throw new FuelException("Run out of fuel!");
        }

        private void PressBrakePedal()
        {
            if (Speed > GetDeAccelerationSpeed())
            {
                Speed -= GetDeAccelerationSpeed();
                Mileage++;
            }
            else
            {
                Speed -= Speed;
                Engine.Stop();
            }
            if (DownForcePressure > 1)
                DownForcePressure -= 1;
            else DownForcePressure = 0;
            PrintCurrentSpeed();
        }

        private double GetDeAccelerationSpeed()
        {
            return 10000/Weight;
        }
    }
}