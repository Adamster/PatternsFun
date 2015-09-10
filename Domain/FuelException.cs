using System;

namespace Domain
{
    public class FuelException : Exception
    {
        private string _fuelMessage = "Run out of fuel!";
    }
}