using System;
using Domain.CarTypes;

namespace Web.Models
{
    public class SportCarModel : CarModel
    {
        public SportCarModel(SportCar car) : base(car)
        {
            DownForcePressure = car.DownForcePressure;
        }

        [Obsolete]
        public SportCarModel()
        {
        }

        public int DownForcePressure { get; set; }
    }
}