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
        }

        public PilotModel()
        {
            DebutDate = DateTime.Today;
        }

        public long Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Range(18, 100)]
        public int Age { get; set; }

        [UIHint("TextBoxEditor")]
        public string Team { get; set; }

        [Display(Name = "Debut date")]
        public DateTime DebutDate { get; set; }

        public List<Vehicle> VehiclesList { get; set; }
    }
}