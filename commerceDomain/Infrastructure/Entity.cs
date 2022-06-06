using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure
{
    public abstract class Entity<TId>
    {

        public TId id{ get; set; }
        public bool Equals(Entity<TId> other)
        {
            return (this.id.Equals(other.id));
        }

    }
}
