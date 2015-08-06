// File: IVisitor.cs in
// PatternsFun by Serghei Adam 
// Created 06 08 2015 
// Edited 06 08 2015

using Domain.CarTypes;
using Domain.Paddock;
using Domain.Persons;

namespace Domain.Interfaces
{
    public interface IVisitor
    {
        void Visit(Boxes box);
        void Visit(PitLane lane);
    }
}