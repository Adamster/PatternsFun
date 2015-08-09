// File: Boxes.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 07 08 2015

using System;
using Domain.Interfaces;

namespace Domain.Paddock
{
    public class Boxes : IAccess, IAcceptVisitor
    {
        public Boxes(string owner, int numberInLine)
        {
            Owner = owner;
            NumberInLine = numberInLine;
        }

        public string Owner { get; private set; }
        public int NumberInLine { get; private set; }

        #region Implementation of IAcceptVisitor

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion

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