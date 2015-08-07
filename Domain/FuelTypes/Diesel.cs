// File: Diesel.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 06 08 2015

using Domain.Interfaces;

namespace Domain.FuelTypes
{
    public class Diesel : IFuelConsumeStrategy
    {
        #region Implementation of IFuelConsumeStrategy

        public double BurnFuelRate(int hp, double weight)
        {
            return weight/hp*0.5/10;
        }

        #endregion
    }
}