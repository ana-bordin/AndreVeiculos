using Models;

namespace Repositories
{
    public interface ISaleRepository
    {
        bool InsertAll(List<Sale> sales);
        bool Insert(Sale sale);
        List<Sale> GetAll();
    }
}
