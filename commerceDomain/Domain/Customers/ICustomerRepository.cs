using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Customers
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(Guid id);
        Customer FindBy(Guid Id);
        Customer FindByEmail(string email);
        void Create(Customer customer);
        Customer FindByIdStream(Guid id);
        void UpdateStream(Customer customer);
    }
}
