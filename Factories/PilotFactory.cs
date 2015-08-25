using System;
using Domain.Persons;

namespace Factories
{
    public static class PilotFactory
    {
        public static Pilot CreateNewPilot(string name, DateTime debutDateTime, int age, string teamName)
        {
            var pilot = new Pilot(name, debutDateTime, age, teamName);

            return pilot;
        }
    }
}