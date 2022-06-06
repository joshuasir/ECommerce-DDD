using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure
{
    public interface IEventStore
    {
        void CreateNewStream(string streamName, IEnumerable<DomainEvent> domainEvents);
        void AppendEventsToStream(string streamName, IEnumerable<DomainEvent> domainEvents, int? expectedVersion);
        IEnumerable<DomainEvent> GetStream(string streamName, int fromVersion, int toVersion);
        void AddSnapshot<T>(string streamName, T snapshot);
        T GetLatestSnapshot<T>(string streamName) where T : class;
    }
}
