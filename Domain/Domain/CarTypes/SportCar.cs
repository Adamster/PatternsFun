// File: SportCar.cs in
// PatternsFun by Serghei Adam 
// Created 29 07 2015 
// Edited 04 08 2015

using System;
using System.Diagnostics;
using Domain.Domain.Engines;
using Domain.Utils;

namespace Domain.Domain.CarTypes
{
    public class SportCar : Car
    {
        public SportCar(double fuelTankValue, double weightValue, GasolineEngine carEngineValue, string nameValue,
            string addspec)
        {
            if (string.IsNullOrWhiteSpace(nameValue)) throw new ArgumentException("please name the SportCar!");
            Name = nameValue;
            if (fuelTankValue < 0) throw new ArgumentException("fuel tank volume can't be below or equal zero");
            FuelTank = fuelTankValue;
            if (weightValue <= 0) throw new ArgumentException("weight can't be below or equal zero");
            Weight = weightValue;
            Engine = carEngineValue;
            AccelerationSpeed = Engine.HorsePowers/Weight*100;
            DownForcePressure = 0;
            SpecialAdds = addspec;
        }

        private int DownForcePressure { get; set; }
        public new GasolineEngine Engine { get; protected set; }

        public override void Accelerate(int toSpeed)
        {
            GeneretaDownForce();
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
                        if (Speed == 0) Speed += AccelerationSpeed;
                        else Speed += AccelerationSpeed - Speed/10;
                        Console.WriteLine("Car Accelerate");
                        PrintCurrentSpeed();
                        Mileage += Speed;
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
        }

        public override void Brake()
        {
            PressBrakePedal();
        }

        private void GeneretaDownForce()
        {
            if (Speed != 0)
            {
                DownForcePressure++;
                Console.WriteLine("Generating downforce");
            }
        }

        private bool BurnFuel()
        {
            if (FuelTank > 0)
            {
                FuelTank -= 5;
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
            DownForcePressure -= 1;
            Console.WriteLine("brake pedal pressed");
            PrintCurrentSpeed();
        }

        private double GetDeAccelerationSpeed()
        {
            return 10000/Weight;
        }
    }
}