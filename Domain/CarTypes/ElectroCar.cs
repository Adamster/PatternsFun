using System;
using Domain.EnginesTypes;
using Domain.Interfaces;
using Domain.Persons;
using Utils;

namespace Domain.CarTypes
{
    public class ElectroCar : Vehicle, ISteeringWheel
    {
        private readonly ElectroEngine _engine;

        public ElectroCar(string name, double? mileage, double speed, double weight, string specialAdds,
            double accelerationSpeed, Pilot pilot, int chargeLevel)
            : base(name, mileage, speed, weight, specialAdds, accelerationSpeed, pilot)
        {
            if (chargeLevel < 0 || chargeLevel > 100)
                throw new ArgumentException("Charge lvl can't be below zero or more than 100");
            ChargeLevel = chargeLevel;
             AccelerationSpeed = _engine.HorsePowers/Weight*100;
        }

     //   public ElectroCar(int chargeLvlValue, int weightValue, ElectroEngine electroEngineValue, string nameValue)
        //{
        //    Name = nameValue;
        //    ChargeLevel = chargeLvlValue;
        //    Weight = weightValue;
        //    _engine = electroEngineValue;    
        //}

        private int ChargeLevel { get; set; }

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

        #region ISteeringWheel Members

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

        #endregion
    }
}