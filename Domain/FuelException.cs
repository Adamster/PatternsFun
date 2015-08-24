using System;

namespace Domain
{
    internal class FuelException : Exception
    {
        private string _fuelMessage = "Run out of fuel!";
    }
}