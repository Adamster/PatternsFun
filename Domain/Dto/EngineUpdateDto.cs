using Domain.EnginesTypes;

namespace Domain.Dto
{
    public class EngineUpdateDto
    {
        public EngineUpdateDto()
        {
        }

        public EngineUpdateDto(int hp, EngineTypes type)
        {
            HorsePowers = hp;
            EngineType = type;
        }

        public int HorsePowers { get; set; }
        public EngineTypes EngineType { get; set; }
    }
}