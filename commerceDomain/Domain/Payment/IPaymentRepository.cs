using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Payment
{
    public interface IPaymentRepository
    {
        void Add(Payment pay);
        List<Payment> ViewTransactions();
        Payment FindBy(Guid paymentid);
        void UpdateStream(Payment payment);
        Payment FindByIdStream(Guid id);
        void CreateStream(Payment payment);
    }
}
