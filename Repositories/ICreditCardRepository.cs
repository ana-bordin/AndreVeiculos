using Models;

namespace Repositories
{
    public interface ICreditCardRepository
    {
        bool InsertAll(List<CreditCard> creditCards);
        bool Insert(CreditCard creditCard);
        List<CreditCard> GetAll();
    }
}
