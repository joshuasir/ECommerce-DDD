
using commerceDomain.Application.Customers.RequestModel;
using commerceDomain.Domain;
using commerceDomain.Domain.Customers;
using commerceDomain.Domain.Customers.Events;
using commerceDomain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Customers
{
    public class UpdFormHandler
    {
        private ICustomerRepository _customerRepository;
        private CommerceDatabaseContext _unitofWork;

        public UpdFormHandler(ICustomerRepository customerRepository, CommerceDatabaseContext unitofWork)
        {
            _customerRepository = customerRepository;
            _unitofWork = unitofWork;
        }
        public void Upd(UpdCustomerRequest newCust)
        {
            var customer = new ValidateCustomer(_customerRepository).Validate(newCust);

            if (customer != null) return;

            _customerRepository.Update(customer);
            _unitofWork.SaveChanges();

            var customerStream = _customerRepository.FindByIdStream(customer.id);
            customerStream.Changes.Add(new UpdFormEvent(customer.id, newCust.name, newCust.street, newCust.postCode, newCust.city));
            _customerRepository.UpdateStream(customerStream);

        }
    }
}
