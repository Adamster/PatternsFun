using System;

namespace Domain.Domain
{
    internal class FuelException : Exception
    {
        private string _fuelMessage = "Run out of fuel!";
    }
}