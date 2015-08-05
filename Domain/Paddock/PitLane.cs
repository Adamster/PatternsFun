// File: PitLane.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 05 08 2015

namespace Domain.Paddock
{
    internal class PitLane
    {
        public PitLane(int lenght)
        {
            Lenght = lenght;
        }

        public int Lenght { get; private set; }
    }
}