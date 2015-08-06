// File: Tuning.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 06 08 2015

using System;
using Domain.CarTypes;
using Domain.Interfaces;

namespace Domain.Patterns.Decorator
{
    public abstract class Tuning<TVehicleComponent> : IVehicleComponent where TVehicleComponent : IVehicleComponent
    {
        #region Implementation of IVehicleComponent

        protected TVehicleComponent InputComponent;

        protected Tuning(TVehicleComponent car)
        {
            InputComponent = car;
        }

        public abstract void TunePart();

        #endregion
    }

    public class TuneEngine : Tuning<Car>, IVehicleComponent
    {
        public TuneEngine(Car car)
            : base(car)
        {
        }

        #region Overrides of Tuning

        public override void TunePart()
        {
            InputComponent.Engine.AddHp(100);
            InputComponent.TunePart();
            Console.WriteLine("Engine tuned");
        }

        #endregion
    }

    public class TuneWheels : Tuning<Car>, IVehicleComponent
    {
        public TuneWheels(Car car) : base(car)
        {
        }

        #region Overrides of Tuning

        public override void TunePart()
        {
            InputComponent.RemoveWeight(60);
            InputComponent.TunePart();
            Console.WriteLine("Wheels tuned");
        }

        #endregion
    }
}