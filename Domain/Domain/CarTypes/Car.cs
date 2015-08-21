// File: Car.cs in
// PatternsFun by Serghei Adam 
// Created 21 08 2015 
// Edited 21 08 2015

#region

using System;
using System.Diagnostics;
using Domain.Domain.Engines;
using Domain.Utils;
using InterfacesActions;

#endregion

namespace Domain.Domain.CarTypes
{
    public class Car : Vehicle, ISteeringWheel
    {
        public Car()
        {
            Name = "Undefined";
            FuelTank = 0;
            Weight = 0;
            Engine = null;
        }

        public Car(int fuelTankValue, double weightValue, GasolineEngine carEngineValue, string nameValue,
            string addParam)
        {
            if (string.IsNullOrWhiteSpace(nameValue)) throw new ArgumentException("please name the car!");
            if (fuelTankValue < 0) throw new ArgumentException("fuel tank volume can't be below or equal zero");
            if (weightValue <= 0) throw new ArgumentException("weight can't be below or equal zero");

            Name = nameValue;
            FuelTank = fuelTankValue;
            Weight = weightValue;
            Engine = carEngineValue;
            AccelerationSpeed = Engine.HorsePowers/Weight*100;
            SpecialAdds = addParam;
        }

        protected double FuelTank { get; set; }
        public GasolineEngine Engine { get; protected set; }

        #region ISteeringWheel Members

        public void TurnLeft()
        {
            Console.WriteLine("Car turning left");
            Logger.AddMsgToLog("Car turning left");
        }

        public void TurnRight()
        {
            Console.WriteLine("Car turning right");
            Logger.AddMsgToLog("Car turning right");
        }

        public void Horn()
        {
            Console.WriteLine("Car beep");
            Logger.AddMsgToLog("Car beep");
        }

        #endregion

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
                        if (Speed == 0) Speed += AccelerationSpeed;
                        else Speed += AccelerationSpeed - Speed/10;
                        Console.WriteLine("Car Accelerate");
                        Mileage += Speed;
                        PrintCurrentSpeed();
                    }
                }

                catch (Exception ex)
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
                    else break;
                }
            }
        }

        public override void Brake()
        {
            PressBrakePedal();
            Console.WriteLine("Car Brake");
        }

        public void FillTank(double value)
        {
            FuelTank += value;
        }

        private bool BurnFuel()
        {
            if (FuelTank > 0)
            {
                FuelTank -= 1;
                Console.WriteLine("burning fuel...");
                Debug.WriteLine("Fuel Burn succesfully, remaining in tank: " + FuelTank);
                return true;
            }
            throw new FuelException();
        }

        private void PressBrakePedal()
        {
            if (Speed > GetDeAccelerationSpeed()) Speed -= GetDeAccelerationSpeed();
            else
            {
                Speed -= Speed;
                Engine.Stop();
            }

            Console.WriteLine("brake pedal pressed");

            PrintCurrentSpeed();
        }

        private double GetDeAccelerationSpeed()
        {
            return 10000/Weight;
        }
    }
}