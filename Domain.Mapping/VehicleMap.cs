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
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Mileage);
            Map(x => x.SpecialAdds);
        }
    }
}