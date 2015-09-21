using Domain.Persons;

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
            HasMany(x => x.CarVehicles).Cascade.SaveUpdate().Inverse().ForeignKeyConstraintName("FK7867CB2454E78567");
        }
    }
}