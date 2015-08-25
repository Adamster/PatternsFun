using System;
using System.Collections.Generic;
using Domain.Interfaces;
using Utils;

namespace Domain.Persons
{
    public class Pilot : Entity, IPilot
    {
        public IList<Vehicle> CarVehicles = new List<Vehicle>();

        public Pilot(string name, DateTime debutDate, int age, string team)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new Exception("Pilot need a name!");
            if (age < 17) throw new Exception("Too young age for a pilot!");
            if (string.IsNullOrWhiteSpace(team)) throw new Exception("Team name can't be empty");

            Name = name;
            DebutDate = debutDate;
            Age = age;
            Team = team;
        }

        protected Pilot()
        {
        }

        public virtual string Name { get; protected set; }
        public virtual string Team { get; protected set; }
        public virtual int Age { get; set; }
        public virtual DateTime DebutDate { get; protected set; }

        public virtual IList<Vehicle> CarVehiclesList
        {
            get { return CarVehicles; }
        }

        public virtual void AddCar(Vehicle car )
        {
            CarVehicles.Add(car);
        }
        public virtual TimeSpan ExpierenceTime
        {
            get { return DateTime.Now.Date - DebutDate.Date; }
            set { }
        }

        public PaddockAccessLevels AccessLevel
        {
            get { return PaddockAccessLevels.ClubPaddock; }
        }

        #region Implementation of IPilot

        public void Update(string status)
        {
            Console.WriteLine("Race status : {0} for {1}", status, Name);
            Logger.AddMsgToLog("race status: " + status + " for " + Name);
        }

        #endregion
    }
}