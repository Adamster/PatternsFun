// File: Boxes.cs in
// PatternsFun by Serghei Adam 
// Created 04 08 2015 
// Edited 04 08 2015

using System;
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

        public void PilotAcces()
        {
            Console.WriteLine("Welcome back to {0}", Owner);
        }

        #endregion
    }
}