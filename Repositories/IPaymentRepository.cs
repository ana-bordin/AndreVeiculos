using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPaymentRepository
    {
        bool InsertAll(List<Payment> payments);
        bool Insert(Payment payment);
        List<Payment> GetAll();
    }
}
