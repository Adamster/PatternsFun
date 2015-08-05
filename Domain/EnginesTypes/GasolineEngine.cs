// File: GasolineEngine.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 05 08 2015

using System;

namespace Domain.EnginesTypes
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