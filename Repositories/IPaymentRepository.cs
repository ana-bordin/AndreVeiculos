using Models;

namespace Repositories
{
    public interface IPaymentRepository
    {
        bool InsertAll(List<Payment> payments);
        bool Insert(Payment payment);
        List<Payment> GetAll();
    }
}
