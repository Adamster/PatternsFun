// File: FuelException.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 05 08 2015

using System;

namespace Domain
{
    internal class FuelException : Exception
    {
        public FuelException(string fuelMessage)
        {
            _fuelMessage = fuelMessage;
        }

        private string _fuelMessage;
    }
}