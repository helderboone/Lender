using System;

namespace Lender.API.Models.Base
{
    public abstract class Entity
    {
        public long Id { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
