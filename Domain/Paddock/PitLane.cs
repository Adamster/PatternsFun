// File: PitLane.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 06 08 2015

using Domain.Interfaces;

namespace Domain.Paddock
{
    public class PitLane : IAcceptVisitor
    {
        public PitLane(int lenght, string name)
        {
            Lenght = lenght;
            Name = name;
        }

        public string Name { get; private set; }
        public int Lenght { get; private set; }

        #region Implementation of IAcceptVisitor

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion
    }
}