using System;
using System.Diagnostics;
using System.Threading;
using Domain.Dto;
using Domain.EnginesTypes;
using Domain.FuelTypes;
using Domain.Interfaces;
using Domain.Persons;
using Utils;

namespace Domain.CarTypes
{
    public class Car : Vehicle, ISteeringWheel, IVehicleComponent, IChangeOil
    {
        protected IFuelConsumeStrategy FuelType;
        protected Stopwatch Sw = new Stopwatch();

        [Obsolete]
        protected Car()
        {
        }

        public Car(string name, double? mileage, double weight, string additionalInfo,
            Pilot pilot, double fuelTank, GasolineEngine engine)
            : base(name, mileage, weight, additionalInfo, pilot)
        {
            if (fuelTank < 0) throw new ArgumentException("fuel tank volume can't be below or equal zero");
            FuelTank = fuelTank;
            Engine = engine;
            FuelType = new Petrol();
            AccelerationSpeed = GetAccelerationSpeed();
        }

        public virtual double Weight
        {
            get { return GetWeight(); }
            protected set { SetWeight(value); }
        }

        public virtual double FuelTank { get; protected set; }
        public virtual GasolineEngine Engine { get; set; }

        public virtual void TurnLeft()
        {
            Console.WriteLine("Car turning left");
            Logger.AddMsgToLog("Car turning left");
        }

        public virtual void TurnRight()
        {
            Console.WriteLine("Car turning right");
            Logger.AddMsgToLog("Car turning right");
        }

        public virtual void Horn()
        {
            Console.WriteLine("Car beep");
            Logger.AddMsgToLog("Car beep");
        }

        #region Implementation of IVehicleComponent

        public virtual void TunePart()
        {
            Console.WriteLine("Welcome to West Coast Customs!");
        }

        #endregion

        public virtual void StopWatch()
        {
            Sw.Stop();
        }

        protected virtual double GetAccelerationSpeed()
        {
            return Engine.HorsePowers/Weight*100;
        }

        public override void Accelerate(int toSpeed)
        {
            PressThrottle(toSpeed);
        }

        public virtual void ContinousAccelerate()
        {
            ContinousPressThrottle();
        }

        protected virtual void ContinousPressThrottle()
        {
            if (Mileage == null)
                Mileage = 0;
            if (!Sw.IsRunning)
            {
                Sw.Start();
                Thread.Sleep(1);
            }

            if (BurnFuel())
            {
                if (Speed == 0) Speed += GetAccelerationSpeed()/5;
                else Speed += GetAccelerationSpeed() - Speed/10;
                var tmp = Math.Ceiling((double) Sw.Elapsed.Milliseconds);
                // Console.WriteLine("time elapsed: {0}, tmp value: {1}", _sw.Elapsed.Seconds, tmp);
                Mileage += tmp*Speed;
                PrintCurrentSpeed();

                Console.WriteLine("Distance traveled: {0}", Mileage);
                Sw.Reset();
            }
        }

        protected virtual void PressThrottle(int toSpeed)
        {
            Engine.Start();
            Mileage = 0;

            while (Speed < toSpeed)
            {
                try
                {
                    if (BurnFuel())
                    {
                        if (Speed == 0) Speed += GetAccelerationSpeed()/5;
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
                    var readLine = Console.ReadLine();
                    if (readLine != null && readLine.ToLower() == "y")
                    {
                        Console.WriteLine("How much?");
                        var addFuelTmp = Console.ReadLine();
                        double addFuel = 0;
                        double.TryParse(addFuelTmp, out addFuel);
                        FillTank(addFuel);
                    }
                    else
                        throw new FuelException();
                }
            }
            Console.WriteLine("Fuel remaining in tank: {0}", FuelTank);
        }

        public virtual void StopTheCar()
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

        public virtual void FillTank(double value)
        {
            FuelTank += value;
        }

        protected virtual bool BurnFuel()
        {
            if (FuelTank > 0)
            {
                FuelTank -= BurnFuelRate(FuelType);
                Console.WriteLine("burning fuel...");
                Debug.WriteLine("Fuel Burn succesfully, remaining in tank: " + FuelTank);
                return true;
            }
            throw new FuelException();
        }

        protected virtual double BurnFuelRate(IFuelConsumeStrategy fuelType)
        {
            return fuelType.BurnFuelRate(Engine.HorsePowers, Weight);
        }

        public virtual void SetFuelType(IFuelConsumeStrategy fuelType)
        {
            FuelType = fuelType;
        }

        protected virtual void PressBrakePedal()
        {
            if (Speed > GetDeAccelerationSpeed()) Speed -= GetDeAccelerationSpeed();
            else
            {
                Speed -= Speed;
                Engine.Stop();
            }
            PrintCurrentSpeed();
        }

        protected virtual double GetDeAccelerationSpeed()
        {
            return 10000/Weight;
        }


        public virtual Car CarEdit(CarUpdateDto updatedCar)
        {
            Id = updatedCar.Id;
            Name = updatedCar.Name;
            AdditionalInfo = updatedCar.AdditionalInfo;
            Engine.UpdateEngineInfo(Engine, updatedCar.Engine);
            if (OwnerPilot == null)
            {
                OwnerPilot = new Pilot();
            }
            OwnerPilot.PilotEdit(updatedCar.Pilot);
            FuelTank = updatedCar.TankVolume;
            Weight = updatedCar.Weight;
            return this;
        }

        #region Implementation of IChangeOil

        public virtual void ChangeOilRequest()
        {
            StopTheCar();
            OpenBonnet();
            OilReplace();
            CloseBonnet();
        }

        protected virtual void OilReplace()
        {
            Console.WriteLine("Oil replaced");
        }

        protected virtual void CloseBonnet()
        {
            Console.WriteLine("Close car bonnet");
        }

        protected virtual void OpenBonnet()
        {
            Console.WriteLine("Open car bonnet");
        }

        #endregion
    }
}