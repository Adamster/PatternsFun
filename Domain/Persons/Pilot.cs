// File: Pilot.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 07 08 2015

using System;
using Domain.Interfaces;
using Utils;

namespace Domain.Persons
{
    public class Pilot : IPilot
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
        }

        public string Name { get; private set; }
        public string Team { get; private set; }
        public int Age { get; private set; }
        public DateTime DebutDate { get; private set; }

        public PaddockAccessLevels AccessLevel
        {
            get { return PaddockAccessLevels.ClubPaddock; }
        }

        public TimeSpan ExpierenceTime
        {
            get { return DateTime.Now.Date - DebutDate.Date; }
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