using System;

namespace Domain.Utils
{
    internal class FuelException : Exception
    {
        private string _fuelMessage = "Run out of fuel!";
    }
}