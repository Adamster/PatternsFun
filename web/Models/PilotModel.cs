using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain;
using Domain.Persons;

namespace Web.Models
{
    public class PilotModel
    {
        public PilotModel(Pilot pilot)
        {
            Id = pilot.Id;
            Name = pilot.Name;
            Age = pilot.Age;
            Team = pilot.Team;
            DebutDate = pilot.DebutDate;
            ExperienceTime = Convert.ToInt32((DateTime.Now - DebutDate).TotalDays);
        }

        public PilotModel()
        {
            //DebutDate = DateTime.Today;
        }

        public long Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Enter correct name")]
        public string Name { get; set; }

        [Range(18, 100)]
        public int Age { get; set; }


        [Required]
        public string Team { get; set; }

        [Display(Name = "Debut date")]
        public DateTime DebutDate { get; set; }

        public int ExperienceTime { get; set; }

        public List<Vehicle> VehiclesList { get; set; }
    }
}