// File: VehicleMap.cs in
// PatternsFun by Serghei Adam 
// Created 21 08 2015 
// Edited 21 08 2015

using Domain.Domain;
using FluentNHibernate.Data;
using FluentNHibernate.Mapping;

namespace Domain.Mapping
{
    public abstract class EntityMap<TEntity> : ClassMap<TEntity> where TEntity : Entity
    {
        protected EntityMap()
        {
            Id(x => x.Id);
            DynamicUpdate();
        }
    }

    public class VehicleMap : EntityMap<Vehicle>
    {
        public VehicleMap()
        {
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Mileage);
            Map(x => x.SpecialAdds);
        }
     
    }
}