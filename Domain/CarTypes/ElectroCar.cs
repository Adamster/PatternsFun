using System;
using Domain.EnginesTypes;
using Domain.Interfaces;
using Domain.Persons;
using Utils;

namespace Domain.CarTypes
{
    public class ElectroCar : Vehicle, ISteeringWheel
    {
        public ElectroCar(string name, double? mileage, ElectroEngine electroEngine, double weight, string additionalInfo,
            Pilot pilot, int chargeLevel)
            : base(name, mileage, weight, additionalInfo, pilot)
        {
            if (chargeLevel < 0 || chargeLevel > 100)
                throw new ArgumentException("Charge lvl can't be below zero or more than 100");
            ChargeLevel = chargeLevel;
            Engine = electroEngine;
            AccelerationSpeed = Engine.HorsePowers/Weight*100;
        }

        [Obsolete]
        protected ElectroCar()
        {
        }

        public virtual ElectroEngine Engine { get; protected set; }
        public virtual int ChargeLevel { get; protected set; }

        public override void Accelerate(int toSpeed)
        {
            PressThrottle(toSpeed);
        }

        protected virtual void PressThrottle(int toSpeed)
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

        protected virtual bool DischargeBattery()
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

        public override void Brake()
        {
            ChargeBatteryByBrakes();
            PrintCurrentSpeed();
        }

        protected virtual void ChargeBatteryByBrakes()
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

        public virtual void Charge(int chargeLvl)
        {
            if (ChargeLevel + chargeLvl > 100)
            {
                Console.WriteLine("charge lvl can't be more than 100%");
                return;
            }
            ChargeLevel += chargeLvl;
        }

        protected virtual double GetDeAccelerationSpeed()
        {
            return 10000/Weight;
        }

        #region ISteeringWheel Members

        public virtual void Horn()
        {
            Console.WriteLine("ElectroCar beep");
        }

        public virtual void TurnLeft()
        {
            Console.WriteLine("ElectroCar turning left");
        }

        public virtual void TurnRight()
        {
            Console.WriteLine("ElectroCar turning right");
        }

        #endregion
    }
}