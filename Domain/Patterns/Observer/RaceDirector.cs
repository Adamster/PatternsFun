// File: RaceDirector.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 05 08 2015

using System.Collections.Generic;
using Domain.Interfaces;

namespace Domain.Patterns.Observer
{
    public class RaceDirector
    {
        public RaceDirector()
        {
            _pilots = new List<IPilot>();
        }

        public string RaceStatus
        {
            get { return _raceStatus; }
            set
            {
                _raceStatus = value;
                Notify();
            }
        }

        private readonly List<IPilot> _pilots;
        private string _raceStatus;

        public void JoinRace(IPilot pilot)
        {
            if (!_pilots.Contains(pilot))
                _pilots.Add(pilot);
        }

        public void LeaveRace(IPilot pilot)
        {
            if (_pilots.Contains(pilot))
                _pilots.Remove(pilot);
        }

        private void Notify()
        {
            foreach (var pilot in _pilots)
            {
                pilot.Update(RaceStatus);
            }
        }
    }
}