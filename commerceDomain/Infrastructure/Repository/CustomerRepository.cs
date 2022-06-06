using commerceDomain.Domain;
using commerceDomain.Domain.Customers;
using commerceDomain.Domain.Products;
using commerceDomain.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly CommerceDatabaseContext _commerceContext;
        private readonly IEventStore _eventStore;

        public CustomerRepository(CommerceDatabaseContext commerceContext, IEventStore eventStore)
        {
            _commerceContext = commerceContext;
            _eventStore = eventStore;
        }

        public void Add(Customer customer)
        {

            var customerDTO = new CustomerDTO()
            {
                Id = customer.id,
                name = customer.name,
                email = customer.email,
                address = customer.address.toString(),
                stsrc = 'I'
            };

            _commerceContext.Customers.Add(customerDTO);
        }


        public void Update(Customer customer)
        {
            var toUpd = _commerceContext.Customers.Find(customer.id);
            toUpd.name = customer.name;
            toUpd.email = customer.email;
            toUpd.newsLetter = customer.newsLetter;
            toUpd.stsrc = 'U';
            toUpd.address = customer.address.toString();
        }

        public void Delete(Guid id)
        {
            var toDel = _commerceContext.Customers.Find(id);
            toDel.stsrc = 'D';
        }

        public Customer FindBy(Guid Id)
        {
            var customerDTO = _commerceContext.Customers.Find(Id);

            if (customerDTO == null)
                throw new ArgumentNullException(nameof(customerDTO));

            string[] address = customerDTO.address.Split(',');

            return Customer.CreateNew(customerDTO.Id, customerDTO.name, customerDTO.email, address[0].Trim(),  address[2].Trim(), address[1].Trim());
        }

        public Customer FindByEmail(string email)
        {
            var customer = _commerceContext.Customers.Where(a => a.email.Equals(email)).FirstOrDefault();

            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            string[] address = customer.address.Split(',');

            return Customer.CreateNew(customer.Id, customer.name, customer.email, address[0].Trim(), address[2].Trim(), address[1].Trim());
        }

        private string StreamNameFor(Guid id)
        {
            // stream per-aggregate: {AggregateType}-{AggregateId}
            return string.Format("{0}-{1}", typeof(Customer).Name, id);
        }

        public void Create(Customer customer)
        {
            var streamName = StreamNameFor(customer.id);
            _eventStore.CreateNewStream(streamName, customer.Changes);
        }

        public void UpdateStream(Customer customer)
        {
            var streamName = StreamNameFor(customer.id);
            _eventStore.AppendEventsToStream(streamName, customer.Changes, null);
        }

        public Customer FindByIdStream(Guid id)
        {
            var streamName = StreamNameFor(id);
            var fromEventNumber = 0;
            var toEventNumber = int.MaxValue;
            var stream = _eventStore.GetStream(
            streamName, fromEventNumber, toEventNumber
            );
            var customer = Customer.CreateNew(id);
            foreach (var @event in stream)
            {
                customer.Apply(@event);
            }
            return customer;
        }
    }
}
