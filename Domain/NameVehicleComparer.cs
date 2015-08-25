
using System.Collections.Generic;

namespace Domain
{
    internal class NameVehicleComparer : IEqualityComparer<Vehicle>
    {
        #region IEqualityComparer<Vehicle> Members

        public bool Equals(Vehicle a, Vehicle b)
        {
            return a.Name == b.Name;
        }

        public int GetHashCode(Vehicle obj)
        {
            return (obj.Name).GetHashCode();
        }

        #endregion
    }
}