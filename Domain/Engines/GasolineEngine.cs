// File: GasolineEngine.cs in
// PatternsFun by Serghei Adam 
// Created 29 07 2015 
// Edited 03 08 2015

using System;

namespace Domain.Engines
{
    public class GasolineEngine : Engine
    {
        public GasolineEngine(int hpValue, EngineTypes engineType)
        {
            HorsePowers = hpValue;
            NumberOfCylinders = (int) engineType;
        }

        protected int NumberOfCylinders { get; set; }

        public override void Start()
        {
            Console.WriteLine("GasolineEngine started");
        }

        public override void Stop()
        {
            Console.WriteLine("GasolineEngine stoped");
        }
    }
}