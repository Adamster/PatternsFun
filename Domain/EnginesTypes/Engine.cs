using System;

namespace Domain.EnginesTypes
{
    public class Engine : Entity
    {
        [Obsolete]
        protected Engine()
        {
        }

        public virtual int HorsePowers { get; protected set; }

        public virtual void Start()
        {
        }

        public virtual void Stop()
        {
        }

        public virtual void AddHp(int value)
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