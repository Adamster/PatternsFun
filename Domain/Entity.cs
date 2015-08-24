using System;

namespace Domain
{
    public abstract class Entity
    {
        public virtual long Id { get; protected set; }

        public static bool operator ==(Entity left, Entity right)
        {
            if (ReferenceEquals(left, right))
                return true;
            if (ReferenceEquals(left, null))
                return false;

            if (ReferenceEquals(right, null))
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        public virtual bool Equals(Entity other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(other, this))
                return true;

            var thisId = Id;
            var otherId = other.Id;

            if (!IsTransistent(this) && !IsTransistent(other) && Equals(thisId, otherId))
            {
                var thisType = GetUnproxiedType();
                var otherType = GetUnproxiedType();

                return thisType.IsAssignableFrom(otherType) ||
                       otherType.IsAssignableFrom(thisType);
            }
            return false;
        }

        protected virtual Type GetUnproxiedType()
        {
            return GetType();
        }

        private static bool IsTransistent(Entity entity)
        {
            return entity != null && Equals(entity.Id, default(long));
        }
    }
}