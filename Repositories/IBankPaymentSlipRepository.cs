using Models;

namespace Repositories
{
    public interface IBankPaymentSlipRepository
    {
        bool InsertAll(List<BankPaymentSlip> bankPaymentSlips);
        bool Insert(BankPaymentSlip bankPaymentSlip);
        List<BankPaymentSlip> GetAll();
    }
}
