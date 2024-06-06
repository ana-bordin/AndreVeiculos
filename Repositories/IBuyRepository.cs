using Models;

namespace Repositories
{
    public interface IBuyRepository
    {
        bool InsertAll(List<Buy> buys);
        bool Insert(Buy buy);
        List<Buy> GetAll();
    }
}
