using System;
using Domain.CarTypes;

namespace Domain.EnginesTypes
{
    public class ElectroEngine : Engine
    {
        [Obsolete]
        protected ElectroEngine()
        {
        }

        public ElectroEngine(int hpValue)
        {
            HorsePowers = hpValue;
            ECE = 0.87;
        }
        public virtual ElectroCar Car { get; set; }
        // dlea liudei ECE == KPD
        public virtual double ECE { get; protected set; }

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