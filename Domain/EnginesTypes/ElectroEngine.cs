// File: ElectroEngine.cs in
// PatternsFun by Serghei Adam 
// Created 05 08 2015 
// Edited 07 08 2015

using System;

namespace Domain.EnginesTypes
{
    public class ElectroEngine : Engine
    {
        public ElectroEngine(int hpValue)
        {
            HorsePowers = hpValue;
        }

        public override void Start()
        {
            Console.WriteLine("ElectroEngine Start");
        }

        public override void Stop()
        {
            Console.WriteLine("ElectroEngine Stop");
        }
    }
}