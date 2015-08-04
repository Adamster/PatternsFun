using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Persons
{
    class Pilot
    {
        public Pilot(string name, DateTime debutDate, string age, string team)
        {
            Name = name;
            DebutDate = debutDate;
            Age = age;
            Team = team;
        }

        public string Name { get; private set; }
        public string Team { get; private set; }
        public string Age { get; private set; }
        public DateTime DebutDate { get; private set; }
        public TimeSpan ExpierenceTime 
        {
            get { return DateTime.Now.Date - DebutDate.Date; }
        }
    }
}
