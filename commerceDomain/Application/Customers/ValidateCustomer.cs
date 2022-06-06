using commerceDomain.Application.Customers.RequestModel;
using commerceDomain.Domain;
using commerceDomain.Domain.Customers;
using commerceDomain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Customers
{
    public class ValidateCustomer
    {
        private ICustomerRepository _customerRepository;

        public ValidateCustomer(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

        }
        public Customer Validate(NewRegistryCustomer command)
        {
            var user = _customerRepository.FindByEmail(command.email);

            return user;
        }
        public Customer Validate(UpdCustomerRequest command)
        {
            var user = _customerRepository.FindByEmail(command.email);

            return user;
        }
    }
}
