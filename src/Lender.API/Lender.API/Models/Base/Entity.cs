using System;

namespace Lender.API.Models.Base
{
    public abstract class Entity
    {
        protected Entity()
        {
            CreationTime = DateTime.Now;
        }

        public long Id { get; set; }

        public DateTime CreationTime { get; private set; }
    }
}
