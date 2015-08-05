using System;

namespace Domain
{
    internal class FuelException : Exception
    {
        private string _fuelMessage;

        public FuelException(string fuelMessage)
        {
            _fuelMessage = fuelMessage;
        }
    }
}