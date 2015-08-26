﻿using Domain.CarTypes;
using FluentNHibernate.Mapping;

namespace Domain.Mapping
{
    public abstract class EntityMap<TEntity> : ClassMap<TEntity> where TEntity : Entity
    {
        protected EntityMap()
        {
            Id(x => x.Id).GeneratedBy.HiLo("100");
            DynamicUpdate();
        }
    }

    public class VehicleMap : EntityMap<Vehicle>
    {
        public VehicleMap()
        {
            References(x => x.OwnerPilot);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Mileage);
        }
    }

    public class CarMap : SubclassMap<Car>
    {
        public CarMap()
        {
            Map(x => x.FuelTank).Not.Nullable();
            Map(x => x.SpecialAdds);
           
        }
    }
}