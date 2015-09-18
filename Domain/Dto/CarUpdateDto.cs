namespace Domain.Dto
{
    public class CarUpdateDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public EngineUpdateDto Engine { get; set; }
        public double Weight { get; set; }
        public double TankVolume { get; set; }
        public string AdditionalInfo { get; set; }
        public PilotUpdateDto Pilot { get; set; }
    }
}