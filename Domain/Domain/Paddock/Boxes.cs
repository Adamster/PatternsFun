using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Interfaces;

namespace Domain.Domain.Paddock
{
    public class Boxes : IAccess
    {
        public Boxes(string owner, int numberInLine)
        {
            Owner = owner;
            NumberInLine = numberInLine;
        }

        public string Owner { get; private set; }
        public int NumberInLine { get; private set; }

        #region Implementation of IAccess

        public void GrantAcces()
        {
            Console.WriteLine("Welcome to {0} team", Owner);
        }

        #endregion
    }
}
