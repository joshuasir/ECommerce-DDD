using commerceDomain.Domain.Customers.Events;
using commerceDomain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain
{
    public class Customer : EventSourcedAggregate<Guid>
    {
        public Customer(Guid id, string name, string email, Address address) : base(id)
        {
            this.id = id;
            this.email = email;
            this.name = name;
            this.address = address;
            this.newsLetter = 'D';
        }

        public Customer(Guid id) : base(id)
        {
            this.id = id;
        }

        public string name { get; private set; }
        public string email { get; private set; }
        public char newsLetter { get; private set; }
        public Address address { get; private set; }
        private void SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            this.name = value;
        }

        private void SetEmail(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            this.email = value;
        }

        private void SetAddress(string street, string postCode, string city)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentNullException(nameof(street));

            this.address = new Address(street, postCode, city);
        }

        public static Customer CreateNew(string name, string email, string city, string postcode,string street)
        {
            return new Customer(Guid.NewGuid(), name, email, new Address(street, postcode, city));
        }

        public static Customer CreateNew(Guid id)
        {
            return new Customer(id);
        }
        public static Customer CreateNew(Guid id,string name, string email, string city, string postcode, string street)
        {
            return new Customer(id, name, email, new Address(street, postcode, city));
        }

        public override void Apply(DomainEvent @event)
        {
            When((dynamic)@event);
            Version = Version++;
        }

        private void When(UpdFormEvent @event)
        {
            SetName(@event.name);
            SetEmail(@event.email);
            SetAddress(@event.street, @event.postCode, @event.city);
        }
    }
}
