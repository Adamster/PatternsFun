using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Paddock
{
    class Boxes
    {
        public Boxes(string owner, int numberInLine)
        {
            Owner = owner;
            NumberInLine = numberInLine;
        }

        public string Owner { get; private set; }
        public int NumberInLine { get; private set; }
    }
}
