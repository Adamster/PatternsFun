using Domain.EnginesTypes;
using Domain.Persons;

namespace Domain.Dto
{
    public class CarUpdateDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public GasolineEngine Engine { get; set; }
        public double Weight { get; set; }
        public double TankVolume { get; set; }
        public string AdditionalInfo { get; set; }
        public Pilot Pilot  { get; set; }


        
    }
}