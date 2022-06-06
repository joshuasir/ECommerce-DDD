using System;
using System.Collections.Generic;

namespace commerceDomain.Infrastructure
{
    public abstract class DomainEvent
    {
        public DateTime? CreatedAt { get; private set; }
        public Guid Id { get; private set; }
        public DomainEvent()
        {
            CreatedAt = DateTime.Now;
            Id = Guid.NewGuid();
        }


        public void SetCreatedAt(DateTime? date)
        {
            if (date == null)
                throw new ArgumentNullException(nameof(date));

            CreatedAt = date;
        }

    }
}
