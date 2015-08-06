// File: NameVehicleComparer.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 06 08 2015

using System.Collections.Generic;

namespace Domain
{
    internal class NameVehicleComparer : IEqualityComparer<Vehicle>
    {
        public bool Equals(Vehicle a, Vehicle b)
        {
            return a.Name == b.Name;
        }

        public int GetHashCode(Vehicle obj)
        {
            return (obj.Name).GetHashCode();
        }
    }
}