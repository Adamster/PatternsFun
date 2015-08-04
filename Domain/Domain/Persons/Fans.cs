// File: Visitor.cs in
// PatternsFun by Serghei Adam 
// Created 04 08 2015 
// Edited 04 08 2015

namespace Domain.Domain.Persons
{
    public class Fans
    {
        public Fans(string name, PaddockAccessLevels accessLevel)
        {
            Name = name;
            AccessLevel = accessLevel;
        }

        public string Name { get; private set; }
        public PaddockAccessLevels AccessLevel { get; private set; }
    }

    public enum PaddockAccessLevels
    {
        ClubPaddock = 1,
        Gold = 2,
        Silver = 3,
        Bronze = 4
    }
}