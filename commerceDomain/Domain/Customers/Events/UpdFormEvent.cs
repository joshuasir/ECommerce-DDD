using commerceDomain.Domain.Orders.Events;
using commerceDomain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Customers.Events
{
    public class UpdFormEvent : DomainEvent
    {
        public Guid id { get; private set; }
        public string name { get; private set; }
        public string email { get; private set; }
        public string street { get; private set; }
        public string postCode { get; private set; }
        public string city { get; private set;}

        public UpdFormEvent(Guid customerId, string name, string street, string postCode, string city)
        {
            this.id = customerId;
            this.name = name;
            this.street = street;
            this.postCode = postCode;
            this.city = city;
        }
    }
}
