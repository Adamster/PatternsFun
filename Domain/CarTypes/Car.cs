// File: Car.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 05 08 2015

#region

using System;
using System.Diagnostics;
using Domain.EnginesTypes;
using Domain.FuelTypes;
using Domain.Interfaces;
using Utils;

#endregion

namespace Domain.CarTypes
{
    public class Car : Vehicle, ISteeringWheel, IVehicleComponent
    {
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
            AccelerationSpeed = GetAccelerationSpeed();
            SpecialAdds = addParam;
            _fuelType = new Petrol();
        }


        protected double FuelTank { get; set; }
        public GasolineEngine Engine { get; protected set; }

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

        #region Implementation of IVehicleComponent

        public void TunePart()
        {
            Console.WriteLine("Welcome to West Coast Customs!");
        }

        #endregion

        protected IFuelConsumeStrategy _fuelType;

        private double GetAccelerationSpeed()
        {
            return Engine.HorsePowers/Weight*100;
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

        public void StopTheCar()
        {
            while (GetSpeed() != 0)
            {
                Brake();
            }
        }

        public override void Brake()
        {
            PressBrakePedal();
        }

        public void FillTank(double value)
        {
            FuelTank += value;
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

        protected double BurnFuelRate(IFuelConsumeStrategy fuelType)
        {
            return fuelType.BurnFuelRate(Engine.HorsePowers, Weight);
        }

        public void SetFuelType(IFuelConsumeStrategy fuelType)
        {
            _fuelType = fuelType;
        }

        private void PressBrakePedal()
        {
            if (Speed > GetDeAccelerationSpeed()) Speed -= GetDeAccelerationSpeed();
            else
            {
                Speed -= Speed;
                Engine.Stop();
            }
            PrintCurrentSpeed();
        }

        private double GetDeAccelerationSpeed()
        {
            return 10000/Weight;
        }
    }
}