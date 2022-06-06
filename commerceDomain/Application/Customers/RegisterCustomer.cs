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
    public class RegisterCustomer
    {
        private ICustomerRepository _customerRepository;
        private CommerceDatabaseContext _unitOfWork;

        public RegisterCustomer(ICustomerRepository customerRepository,
        CommerceDatabaseContext unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }
        public Guid Create(NewRegistryCustomer command)
        {
            var user = new ValidateCustomer(_customerRepository).Validate(command);

            if (user != null) return user.id;

            var userId = Guid.NewGuid();
            var customer = Customer.CreateNew(command.name, command.email, command.city, command.postCode, command.street);
            
            _customerRepository.Create(customer);

            _customerRepository.Add(customer);
            _unitOfWork.SaveChanges();

            return userId;
        }
    }
}
