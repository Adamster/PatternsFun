using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Paddock
{
    class PitLane
    {
        public PitLane(int lenght)
        {
            Lenght = lenght;
        }

        public int Lenght { get; private set; }
    }
}
