using Domain.Persons;
using FluentNHibernate.Mapping;

namespace Domain.Mapping
{
    public class PilotMap : EntityMap<Pilot>
    {
        public PilotMap()
        {
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Age).Not.Nullable();
            Map(x => x.DebutDate).Not.Nullable();
            Map(x => x.Team).Nullable();
            Map(x => x.ExpierenceTime).Not.Nullable();

            HasMany(x => x.CarVehiclesList).Cascade
                .SaveUpdate();
        }
    }
}