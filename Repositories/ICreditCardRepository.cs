using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICreditCardRepository
    {
        bool InsertAll(List<CreditCard> creditCards);
        bool Insert(CreditCard creditCard);
        List<CreditCard> GetAll();
    }
}
