using Domain.EnginesTypes;
using FluentNHibernate.Mapping;

namespace Domain.Mapping
{
    public class EngineMap : EntityMap<Engine>
    {
        public EngineMap()
        {
            
            Map(x => x.HorsePowers);
        }
    }

    public class GasolineEngineMap : SubclassMap<GasolineEngine>
    {
        public GasolineEngineMap()
        {
           
            Map(x => x.NumberOfCylinders);
        }
    }

    public class ElectroEngineMap : SubclassMap<ElectroEngine>
    {
        public ElectroEngineMap()
        {
            Map(x => x.ECE);
        }
    }
}