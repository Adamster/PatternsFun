// File: SportCar.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 07 08 2015

using System;
using System.Diagnostics;
using Domain.EnginesTypes;
using Domain.Interfaces;
using Utils;

namespace Domain.CarTypes
{
    public class SportCar : Car, IChangeOil
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
            return (Engine.HorsePowers/Weight*100);
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
                        else Speed += GetAccelerationSpeed() - Speed/20;
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
                    var readLine = Console.ReadLine();
                    if (readLine != null && readLine.ToLower() == "y")
                    {
                        Console.WriteLine("How much?");
                        var addFuelTmp = Console.ReadLine();
                        double addFuel = 0;
                        double.TryParse(addFuelTmp, out addFuel);
                        FillTank(addFuel);
                    }
                    else if (readLine != null && readLine.ToLower() == "n")
                    {
                        FuelTank = 0;
                        break;
                    }

                    else
                        throw new FuelException();
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
            throw new FuelException();
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

        protected override void CloseBonnet()
        {
            Console.WriteLine("Close SportCar bonnet");
        }

        protected override void OpenBonnet()
        {
            Console.WriteLine("Open SportCar bonnet");
        }
    }
}