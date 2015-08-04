using System.Collections.Generic;

namespace Domain.Domain
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