using Domain.EnginesTypes;

namespace Domain.Dto
{
    public class EngineUpdateDto
    {
        public EngineUpdateDto()
        {
        }

        public EngineUpdateDto(GasolineEngine engine)
        {
            HorsePowers = engine.HorsePowers;
            EngineType = (EngineTypes) engine.NumberOfCylinders;
        }

        public int HorsePowers { get; set; }
        public EngineTypes EngineType { get; set; }
    }
}