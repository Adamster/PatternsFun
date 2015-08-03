using System;

namespace Domain.Domain.Engines
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