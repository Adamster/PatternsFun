// File: Fan.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 05 08 2015

using System;

namespace Domain.Persons
{
    public class Fan
    {
        public Fan(string name, PaddockAccessLevels accessLevel)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new Exception("Tell us your name please!");

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