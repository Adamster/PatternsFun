using Domain.Persons;

namespace Domain.Dto
{
    public class PilotUpdateDto
    {
        public PilotUpdateDto(Pilot pilot)
        {
            if (pilot == null) return;
            Id = pilot.Id;
            Name = pilot.Name;
            Debutdate = pilot.DebutDate.ToString();
            Age = pilot.Age;
            Team = pilot.Team;
        }

        public PilotUpdateDto()
        {
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public string Debutdate { get; set; }

        public int Age { get; set; }

        public string Team { get; set; }
    }
}