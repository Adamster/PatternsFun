using System.Collections.Generic;
using Domain.Domain;

namespace Domain.Utils
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