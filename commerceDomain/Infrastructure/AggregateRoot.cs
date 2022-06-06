using commerceDomain.Domain.Orders.Events;
using commerceDomain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure
{
    public abstract class AggregateRoot<TId> : Entity<TId>
    {
        public AggregateRoot(TId id)
        {
            base.id = id;
        }

    }
}
