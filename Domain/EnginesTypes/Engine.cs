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
}