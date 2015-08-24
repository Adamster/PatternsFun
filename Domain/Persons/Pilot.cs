using System;
using System.Collections.Generic;

namespace Domain.Persons
{
    public class Pilot : Entity
    {
        public Pilot(string name, DateTime debutDate, int age, string team)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new Exception("Pilot need a name!");
            if (age < 17) throw new Exception("Too young age for a pilot!");
            if (string.IsNullOrWhiteSpace(team)) throw new Exception("Team name can't be empty");

            Name = name;
            DebutDate = debutDate;
            Age = age;
            Team = team;
            CarVehicles = new List<Vehicle>();
        }

        

        public virtual string Name { get; protected set; }
        public virtual string Team { get; protected set; }
        public virtual int Age { get; set; }
        public virtual DateTime DebutDate { get; protected set; }
        public virtual IList<Vehicle> CarVehicles { get; set; }
        public virtual TimeSpan ExpierenceTime
        {
            get { return DateTime.Now.Date - DebutDate.Date; }
            set {  }
        }


        protected Pilot()
        {
            
        }
    }
}