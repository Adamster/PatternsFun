﻿// File: Engine.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 07 08 2015

namespace Domain.EnginesTypes
{
    public abstract class Engine
    {
        public int HorsePowers { get; protected set; }
        public abstract void Start();
        public abstract void Stop();

        public void AddHp(int value)
        {
            HorsePowers += value;
        }
    }


    public enum EngineTypes
    {
        V2 = 2,
        V4 = 4,
        V6 = 6,
        V8 = 8,
        V10 = 10,
        V12 = 12,
        V16 = 16
    }
}