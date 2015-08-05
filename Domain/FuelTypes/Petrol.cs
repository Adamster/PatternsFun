// File: Petrol.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 05 08 2015

using Domain.Interfaces;

namespace Domain.FuelTypes
{
    public class Petrol : IFuelConsumeStrategy
    {
        #region Implementation of IFuelConsumeStrategy

        public double BurnFuelRate(int hp, double weight)
        {
            return weight/hp;
        }

        #endregion
    }
}