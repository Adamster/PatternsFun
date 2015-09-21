using Domain.CarTypes;
using FluentNHibernate.Mapping;

namespace Domain.Mapping
{
    public class VehicleMap : EntityMap<Vehicle>
    {
        public VehicleMap()
        {
            References(x => x.OwnerPilot).Column("Pilot_id").Cascade.SaveUpdate();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Mileage);
            Map(x => x.Weight).Not.Nullable();
        }
    }

    public class CarMap : SubclassMap<Car>
    {
        public CarMap()
        {
            References(x => x.Engine).Cascade.All().Unique();
            Map(x => x.FuelTank).Not.Nullable();
            Map(x => x.AdditionalInfo);
        }
    }

    public class SportCarMap : SubclassMap<SportCar>
    {
        public SportCarMap()
        {
            Map(x => x.DownForcePressure).Not.Nullable();
        }
    }

    public class ElectroCarMap : SubclassMap<ElectroCar>
    {
        public ElectroCarMap()
        {
            References(x => x.Engine).Cascade.All().Unique();

            Map(x => x.ChargeLevel).Not.Nullable();
        }
    }
}