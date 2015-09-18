using System;
using Domain.CarTypes;
using Domain.Dto;

namespace Domain.EnginesTypes
{
    public class GasolineEngine : Engine
    {
        public GasolineEngine(int hpValue, EngineTypes engineType)
        {
            HorsePowers = hpValue;
            NumberOfCylinders = (int) engineType;
        }

        [Obsolete]
        protected GasolineEngine()
        {
        }

        public virtual Car Car { get; set; }
        public virtual int NumberOfCylinders { get; protected set; }

        public override void Start()
        {
            Console.WriteLine("GasolineEngine started");
        }

        public override void Stop()
        {
            Console.WriteLine("GasolineEngine stoped");
        }

        public virtual void UpdateEngineInfo(Engine oldEngine, EngineUpdateDto updatedEngine)
        {
            HorsePowers = updatedEngine.HorsePowers;
            NumberOfCylinders = (int) updatedEngine.EngineType;
        }
    }
}