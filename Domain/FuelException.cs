// File: FuelException.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 06 08 2015

using System;

namespace Domain
{
    public class FuelException : Exception
    {
        #region Overrides of Exception

        public override string Message
        {
            get { return "Run out of Fuel!"; }
        }

        #endregion
    }
}