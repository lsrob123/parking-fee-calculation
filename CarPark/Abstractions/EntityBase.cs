using System;

namespace CarPark.Abstractions
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            Key = Guid.NewGuid();
        }

        public abstract Guid Key { get; set; }
    }
}