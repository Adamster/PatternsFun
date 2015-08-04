// File: Tuning.cs in
// PatternsFun by Serghei Adam 
// Created 04 08 2015 
// Edited 04 08 2015

using System;
using Domain.Domain.CarTypes;
using Domain.Domain.Interfaces;

namespace Domain.Domain.Decorator
{
    public abstract class Tuning<TVehicleComponent> : IVehicleComponent where TVehicleComponent : IVehicleComponent
    {
        #region Implementation of IVehicleComponent

        protected TVehicleComponent InputComponent;

        protected Tuning(TVehicleComponent part)
        {
            InputComponent = part;
        }

        public abstract void TunePart();

        #endregion
    }

    public class TuneEngine : Tuning<Car>, IVehicleComponent
    {
        public TuneEngine(Car part)
            : base(part)
        {
           
        }

        #region Overrides of Tuning

        public override void TunePart()
        {
            InputComponent.TunePart();
            Console.WriteLine("Engine tuned");
        }

        #endregion
    }

    public  class TuneSuspension : Tuning<Car>, IVehicleComponent
    {
        public TuneSuspension(Car part) : base(part)
        {
        }

        #region Overrides of Tuning

        public override void TunePart()
        {
            InputComponent.TunePart();
            Console.WriteLine("Suspension tuned");
        }

        #endregion
    }
}