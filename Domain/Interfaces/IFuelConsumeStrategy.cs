// File: IFuelConsumeStrategy.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 05 08 2015

namespace Domain.Interfaces
{
    public interface IFuelConsumeStrategy
    {
        double BurnFuelRate(int hp, double weight);
    }
}