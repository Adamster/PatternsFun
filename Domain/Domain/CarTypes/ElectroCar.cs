// File: ElectroCar.cs in
// PatternsFun by Serghei Adam 
// Created 29 07 2015 
// Edited 03 08 2015

using System;
using Domain.Domain.Engines;
using Domain.Utils;
using InterfacesActions;

namespace Domain.Domain.CarTypes
{
    public class ElectroCar : Vehicle, ISteeringWheel
    {
        public ElectroCar(int chargeLvlValue, int weightValue, ElectroEngine electroEngineValue, string nameValue)
        {
            if (string.IsNullOrWhiteSpace(nameValue)) throw new ArgumentException("please name the ElectroCar!");
            if (chargeLvlValue < 0 || chargeLvlValue > 100)
                throw new ArgumentException("Charge lvl can't be below zero or more than 100");
            if (weightValue <= 0) throw new ArgumentException("weight can't be below or equal zero");

            Name = nameValue;
            ChargeLevel = chargeLvlValue;
            Weight = weightValue;
            _engine = electroEngineValue;
            AccelerationSpeed = _engine.HorsePowers/Weight*100;
        }

        private int ChargeLevel { get; set; }

        public void Horn()
        {
            Console.WriteLine("ElectroCar beep");
        }

        public void TurnLeft()
        {
            Console.WriteLine("ElectroCar turning left");
        }

        public void TurnRight()
        {
            Console.WriteLine("ElectroCar turning right");
        }

        private readonly ElectroEngine _engine;

        public override sealed void Accelerate(int toSpeed)
        {
            PressThrottle(toSpeed);
        }

        private void PressThrottle(int toSpeed)
        {
            Mileage = 0;
            while (Speed < toSpeed)
            {
                try
                {
                    if (DischargeBattery())
                    {
                        Speed += AccelerationSpeed;
                        PrintCurrentSpeed();
                        Logger.AddMsgToLog(Name + "accelerating");
                        Mileage += Speed;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
        }

        private bool DischargeBattery()
        {
            if (ChargeLevel > 6)
            {
                ChargeLevel -= 6;
#if ChargeLvlTest
                 Console.WriteLine("remaminng charge lvl : " + ChargeLevel);
#endif
                return true;
            }
            ChargeLevel -= ChargeLevel;
            throw new Exception("\nLow Battery!\n");
        }

        public override sealed void Brake()
        {
            ChargeBatteryByBrakes();
            PrintCurrentSpeed();
        }

        private void ChargeBatteryByBrakes()
        {
            Console.WriteLine("remaminng charge lvl : " + ChargeLevel);
            Logger.AddMsgToLog("remaminng charge lvl : " + ChargeLevel);

            if (Speed > GetDeAccelerationSpeed())
            {
                Speed -= GetDeAccelerationSpeed();
                ChargeLevel += 1;
                Mileage++;
            }
            else
            {
                Speed -= Speed;
                ChargeLevel += 1;
                Mileage++;
            }
        }

        public void Charge(int chargeLvl)
        {
            if (ChargeLevel + chargeLvl > 100)
            {
                Console.WriteLine("charge lvl can't be more than 100%");
                return;
            }
            ChargeLevel += chargeLvl;
        }

        private double GetDeAccelerationSpeed()
        {
            return 10000/Weight;
        }
    }
}