﻿using Domain.CarTypes;
using FluentNHibernate.Mapping;

namespace Domain.Mapping
{
    public class VehicleMap : EntityMap<Vehicle>
    {
        public VehicleMap()
        {
            References(x => x.OwnerPilot).Column("Pilot_id");
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Mileage);
            Map(x => x.Weight).Not.Nullable();
        }
    }

    public class CarMap : SubclassMap<Car>
    {
        public CarMap()
        {
            References(x => x.Engine).Cascade.All();


            Map(x => x.FuelTank).Not.Nullable();
            Map(x => x.SpecialAdds);
        }
    }

    public class SportCarMap : SubclassMap<SportCar>
    {
        public SportCarMap()
        {
            Map(x => x.DownForcePressure);
        }
    }

    public class ElectroCarMap : SubclassMap<ElectroCar>
    {
        public ElectroCarMap()
        {
            HasOne(x => x.Engine);

            Map(x => x.ChargeLevel);
        }
    }
}